using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class Asteroid : PoolObject, IDamagable, IMineable
    {
        [SerializeField] private float moveSpeedMax = 15f;
        [SerializeField] private float rotationSpeedMax = 10f;
        [SerializeField] private float scaleMax = 1f;
        [SerializeField] private float scaleMin = .2f;

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
            float s = Random.Range(scaleMin, scaleMax);
            transform.localScale *= s;

            float percentage = (s - scaleMin) / (scaleMax - scaleMin);
            rotationSpeed = rotationSpeedMax * (1 - percentage);
            moveSpeed = moveSpeedMax * (1 - percentage * .5f);

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