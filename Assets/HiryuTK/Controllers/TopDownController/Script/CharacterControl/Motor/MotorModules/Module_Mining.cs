using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class Module_Mining : ModuleBase
    {
        const float LaserWidth = 0.4f;

        private ObjectPoolManager_TopDown poolM;
        //private BulletSpawner_TopDown bulletSpawner;
        private float miningCooldownTimer = -1;
        private UIManager_TopDown uiM;

        public Module_Mining(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
        {
            //bulletSpawner = BulletSpawner_TopDown.Instance;
            poolM = ObjectPoolManager_TopDown.instance;
            uiM = UIManager_TopDown.Instance;
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.K))
            {
                Debug.Log("player pressed mining button");
                ShootMiningBeam();
            }
            else if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.Escape))
            {
                TurnOffMiningBeam();
            }
            TickTimer();
        }

        private void TickTimer()
        {
            if (miningCooldownTimer > 0)
            {
                miningCooldownTimer -= Time.deltaTime;
                if (miningCooldownTimer <= 0f)
                {
                    TurnOffMiningBeam();
                }
            }
        }

        private void ShootMiningBeam()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDir = mousePos - (Vector2)player.ShootPoint.position;
            RaycastHit2D hit = Physics2D.Raycast(player.ShootPoint.position, shootDir, settings.MiningDistance, settings.GroundLayer);

            player.LineRenderer.enabled = true;

            if (hit.collider != null) //maybe hit.collider != null 
            {
                //Debug.DrawLine(player.ShootPoint.position, hit.point, Color.red, 1f);
                player.LineRenderer.startColor = Color.yellow;
                player.LineRenderer.endColor = Color.yellow;
                player.LineRenderer.SetPositions(new Vector3[] { player.ShootPoint.position, hit.point });
                player.LineRenderer.widthMultiplier = 0.1f;

                if (MiningCooldownReady)
                {
                    IMineable asteroid = hit.collider.GetComponent<IMineable>();
                    if (asteroid != null)
                    {
                        MineAsteroid(asteroid);
                        miningCooldownTimer = settings.CD_Mining;
                    }
                    else
                    {
                        Debug.Log("Error");
                    }
                }
            }
            else
            {
                //Debug.DrawLine(player.ShootPoint.position, mousePos, Color.blue, 1f);
                player.LineRenderer.SetPositions(new Vector3[] { player.ShootPoint.position, mousePos });
                player.LineRenderer.widthMultiplier = 0.05f;
                player.LineRenderer.startColor = Color.grey;
                player.LineRenderer.endColor = Color.grey;
            }
        }

        private void TurnOffMiningBeam()
        {
            player.LineRenderer.enabled = false;
        }

        private void MineAsteroid(IMineable asteroid)
        {
            asteroid.Mine(settings.MiningPower);
            player.AddMoney(10);
        }

        private bool MiningCooldownReady => miningCooldownTimer <= 0f;

        private void ResetTimer() => miningCooldownTimer = settings.CD_Mining;
    }
}