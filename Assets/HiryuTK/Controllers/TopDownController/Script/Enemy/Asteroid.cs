using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class Asteroid : PoolObject, IDamagable, IMineable
    {
        private const float ScaleMax = 1f;
        private const float ScaleMin = .4f;

        bool alive;
        float moveSpeed;
        float rotationSpeed;
        Vector3 rawForward;
        Vector3 transForward;
        Settings_TopDownController settings;

        #region Interface

        public void TakeDamage(int amount)
        {
            Despawn();
        }

        public void Mine(float amount)
        {
            //Each value will shrink the asteroid by 1%
            float x = transform.localScale.x;
            x -= .01f * amount;

            if (x > 0.35f)
            {
                //Shrink
                Vector3 scale = new Vector3(x, x, x);
                transform.localScale = scale;
            }
            else
            {
                Despawn();
            }
        }
        #endregion

        #region Base class
        public override void InitialSpawn(Pool pool)
        {
            base.InitialSpawn(pool);
            //Reference
            settings = Settings_TopDownController.Instance;
        }

        public override void Activation(Vector2 p, Quaternion r)
        {
            base.Activation(p, r);

            //Initialize scale, speed, and rotation
            float s = Random.Range(ScaleMin, ScaleMax);
            transform.localScale *= s;

            float percentage = (s - ScaleMin) / (ScaleMax - ScaleMin);
            rotationSpeed = settings.AsteroidRotation * (1 - percentage);
            moveSpeed = settings.AsteroidMove * (1 - percentage * .5f);

            rawForward = transform.up;

            StartCoroutine(DetectOutOfBounds());
        }

        protected override void Despawn()
        {
            alive = false;
            base.Despawn();
        }
        #endregion

        private IEnumerator DetectOutOfBounds()
        {
            alive = true;
            yield return new WaitForSeconds(30f);
            while (alive)
            {
                if (settings.IsOutOfBounds(transform.position))
                {
                    Despawn();
                }
                yield return null;
            }
        }

        void FixedUpdate()
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.Translate(rawForward * Time.deltaTime * moveSpeed, Space.World);
            transform.Rotate(new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}