using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class EnemyShip_TopDown : PoolObject, IDamagable
    {
        private const float avoidanceWeight = 2f;

        float neighborRadius;
        private List<Transform> asteroids;
        private PlayerTopDown3DController player;
        Settings_TopDownController settings;
        private Collider2D collider;
        private Vector2 moveDir;
        private bool checkingForCollision;
        private bool alive;


        #region Object pool
        public override void InitialSpawn(Pool pool)
        {
            base.InitialSpawn(pool);
            collider = GetComponent<Collider2D>();
            settings = Settings_TopDownController.Instance;
            player = PlayerTopDown3DController.Instance;
        }

        public override void Activation(Vector2 p, Quaternion r)
        {
            base.Activation(p, r);
            transform.position = p;
            transform.rotation = r;
            moveDir = transform.up;
            //Debug.DrawRay(transform.position, moveDir, Color.yellow, 10f);
            StartCoroutine(DetectOutOfBounds());
        }

        //Non-public
        protected override void Despawn()
        {
            Debug.Log("Ship destroyed");
            checkingForCollision = false;
            base.Despawn();
        }
        #endregion

        #region Mono


        private void FixedUpdate()
        {
            //transform.rotation = Quaternion.LookRotation(moveDir, -Vector3.forward);  //Lerp this later

            //Rotate towards player
            Vector3 dirToPlayer = player.transform.position - transform.position;
            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, dirToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, settings.EnemyRotation * Time.deltaTime);

            transform.Translate(transform.up * settings.EnemyMove * Time.deltaTime, Space.World);
            //Debug.DrawRay(transform.position, transform.up, Color.magenta, 10f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (settings.IsTargetPlayer(collision.gameObject))
            {
                PlayerTopDown3DController p = collision.GetComponent<PlayerTopDown3DController>();
                if (p != null)
                {
                    p.DamagePlayer(transform.position, 1);
                    Despawn();
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, neighborRadius);
        }
        #endregion

        //Public
        public void TakeDamage(int amount)
        {
            Despawn();
        }

        #region Asteroid avoidance

        private IEnumerator CheckForAsteroidCollision()
        {
            checkingForCollision = true;
            while (checkingForCollision)
            {
                DetectAsteroids();
                AvoidAsteroids();
                yield return null;
            }
        }

        private void DetectAsteroids()
        {
            asteroids = new List<Transform>();

            Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

            foreach (Collider2D c in overlaps)
            {
                if (c != collider)
                {
                    Asteroid asteroid = c.GetComponent<Asteroid>();
                    if (asteroid != null)
                    {
                        asteroids.Add(asteroid.transform);
                    }
                }
            }
        }

        private void AvoidAsteroids()
        {
            if (HasAsteroidsNearBy())
            {
                Vector2 avoidanceDir = Vector2.zero;

                foreach (Transform a in asteroids)
                {
                    avoidanceDir += (Vector2)(transform.position - a.position);
                }
                avoidanceDir /= asteroids.Count;

                moveDir += avoidanceDir * avoidanceWeight;
            }
        }

        private bool HasAsteroidsNearBy() => asteroids.Count > 0;
        private Transform GetClosestAsteroid()
        {
            Transform closest = null;
            if (asteroids.Count == 1)
            {
                closest = asteroids[0];
            }
            else
            {
                float closestDist = float.MaxValue;
                foreach (Transform a in asteroids)
                {
                    float dist = Vector2.SqrMagnitude(a.position - transform.position);
                    if (dist < closestDist)
                    {
                        closest = a;
                        closestDist = dist;
                    }
                }
            }
            return closest;
        }
        #endregion

        private IEnumerator DetectOutOfBounds()
        {
            alive = true;
            yield return new WaitForSeconds(3f);
            while (alive)
            {
                if (settings.IsOutOfBounds(transform.position))
                {
                    Despawn();
                }
                yield return null;
            }
        }
    }
}