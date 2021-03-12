using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_BasicAttack : ModuleBase
	{
        private BulletSpawner_TopDown bulletSpawner;
        private float shootCooldownTimer = -1;

        public Module_BasicAttack(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) 
        {
            bulletSpawner = BulletSpawner_TopDown.Instance;
        }

		public override void TickUpdate()
		{
			if (Input.GetMouseButtonDown(0) && ShootingCooldownReady)
			{
                Shoot();
            }
            TickTimer();
        }

        private void TickTimer()
        {
            if (shootCooldownTimer > 0)
            {
                shootCooldownTimer -= Time.deltaTime;
            }
        }

        private void Shoot()
        {
            MonoBehaviour.Instantiate(bulletSpawner.Bullet, 
                player.ShootPoint.position, player.ShootPoint.rotation);
        }

        private bool ShootingCooldownReady => shootCooldownTimer <= 0f;

        private void ResetTimer() => shootCooldownTimer = 0.25f;
    }
}