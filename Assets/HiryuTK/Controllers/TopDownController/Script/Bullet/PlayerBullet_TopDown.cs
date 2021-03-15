using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class PlayerBullet_TopDown : PoolObject
    {
        Settings_TopDownController settings;
        Rigidbody2D rb;

        public override void InitialSpawn(Pool pool)
        {
            base.InitialSpawn(pool);
            settings = Settings_TopDownController.Instance;
            rb = GetComponent<Rigidbody2D>();
        }

        public override void Activation(Vector2 p, Quaternion r)
        {
            base.Activation(p, r);
            rb.velocity = transform.up * settings.BasicBullet_speed;
        }

        private void FixedUpdate()
        {
            if (settings.IsOutOfBounds(transform.position))
            {
                Despawn();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Settings_TopDownController.Instance.IsTargetOnEnemyLayer(collision.gameObject) ||
                Settings_TopDownController.Instance.IsTargetOnGroundLayer(collision.gameObject))
            {
                collision.gameObject.GetComponent<IDamagable>().TakeDamage(1);
                Despawn();
            }
        }
    }
}