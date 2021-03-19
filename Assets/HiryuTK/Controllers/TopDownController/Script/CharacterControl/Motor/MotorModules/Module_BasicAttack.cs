using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_BasicAttack : ModuleBase
	{
        private ObjectPoolManager_TopDown poolM;
        //private BulletSpawner_TopDown bulletSpawner;
        private float shootCooldownTimer = -1;

        public Module_BasicAttack(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) 
        {
            //bulletSpawner = BulletSpawner_TopDown.Instance;
            poolM = ObjectPoolManager_TopDown.instance;
        }

		public override void TickUpdate()
		{
            base.TickUpdate();
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.J))
			{
                if (ShootingCooldownReady)
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
            Quaternion r = Quaternion.LookRotation(Vector3.forward,
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.ShootPoint.position);

            poolM.SpawnPlayerBullet(player.ShootPoint.position, r);
            ResetTimer();
            //MonoBehaviour.Instantiate(bulletSpawner.Bullet, 
            //    player.ShootPoint.position, player.ShootPoint.rotation);
        }

        private bool ShootingCooldownReady => shootCooldownTimer <= 0f;

        private void ResetTimer() => shootCooldownTimer = settings.CD_Mining;
    }
}