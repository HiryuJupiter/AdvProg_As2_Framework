using System.Collections;
using UnityEngine;

namespace HiryuTK.AsteroidsTopDownController
{
    /// <summary>
    /// Class for handling player shooting
    /// </summary>
    public class PlayerShootingModule : MonoBehaviour
    {
        //Fields
        ObjectPoolManager poolM;
        PlayerTopDown3DController player;
        Settings settings;
        float shootCooldownTimer;
        bool isSetup;

        bool ShootingCooldownReady => shootCooldownTimer <= 0f;
        Quaternion RotationTowardsMouse => Quaternion.LookRotation(Vector3.forward,
                Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.ShootPoint.position);
        /// <summary>
        /// Setting up the method
        /// </summary>
        /// <param name="player"> pass in reference to the player</param>
        public void Setup(PlayerTopDown3DController player)
        {
            //Reference and initialization
            this.player = player;
            poolM = ObjectPoolManager.Instance;
            settings = Settings.Instance;
            isSetup = true;
        }

        /// <summary>
        /// Ticks the update for this class
        /// </summary>
        public void TickUpdate()
        {
            //Guard statement
            if (!isSetup)
                return;

            //Shoot when these keys are down
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
            {
                if (ShootingCooldownReady)
                    Shoot();
            }

            //Tick timer 
            if (!ShootingCooldownReady)
            {
                shootCooldownTimer -= Time.deltaTime;
            }
        }

        /// <summary>
        /// Shoot at the mouse pointing location
        /// </summary>
        void Shoot()
        {
            poolM.SpawnPlayerBullet(player.ShootPoint.position, RotationTowardsMouse);
            ResetTimer();

        }

        #region Helper expressions
        //Helper expression bodies for help creating self-documenting code
        void ResetTimer() => shootCooldownTimer = settings.MiningCD;
        
        #endregion
    }
}