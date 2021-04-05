using System.Collections;
using UnityEngine;

namespace HiryuTK.AsteroidsTopDownController.Enemy
{
    /// <summary>
    /// A spawner for spawning asteroids and enemy ships
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        //Fields
        [SerializeField] float spawnIntervalMin = 2f;
        [SerializeField] float spawnIntervalMax = 10f;

        Settings settings;
        ObjectPoolManager poolM;

        float timer = 0f;
        float speedUpMod = 1f;
        float speedUpSpeed = 0.01f;

        void Start()
        {
            //Reference then start spawning
            settings    = Settings.Instance;
            poolM       = ObjectPoolManager.Instance;
            Spawn();
        }

        void Spawn()
        {
            StartCoroutine(DoSpawn());
        }

        /// <summary>
        /// Coroutine that goes on forever to spawn the environmental objects
        /// </summary>
        /// <returns></returns>
        IEnumerator DoSpawn()
        {
            //Infinite loop
            while (true)
            {
                //Ticks timer when it is above zero
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    //50% chance to spawn a ship or an asteroid
                    if (Random.Range(0, 2) == 0)
                        SpawnAsteroid();
                    else
                        SpawnEnemyShip();
                    RefreshTimer();
                }
                yield return null;
            }
        }

        /// <summary>
        /// Reset timer to a regular interval
        /// </summary>
        void RefreshTimer()
        {
            timer = Random.Range(spawnIntervalMin, spawnIntervalMax);
            //Gradually shorten the spawn interval
            while (speedUpMod> 0.1f)
            {
                speedUpMod -= speedUpSpeed;
            }
            timer *= speedUpMod;
        }

        /// <summary>
        /// Method for spawning an asteroid
        /// </summary>
        void SpawnAsteroid()
        {
            Vector3 p = settings.RandomSpawnPoint();
            Quaternion r = settings.RandomSpawnRotation(p);
            Debug.DrawRay(p, r * Vector3.up * 10f, Color.red, 10f);
            poolM.SpawnAsteroid(p, r);
        }

        /// <summary>
        /// Method for spawning an enemy ship
        /// </summary>
        void SpawnEnemyShip ()
        {
            Vector3 p = settings.RandomSpawnPoint();
            Quaternion r = settings.RandomSpawnRotation(p);
            Debug.DrawRay(p, r * Vector3.up * 10f, Color.red, 10f);
            poolM.SpawnEnemyShip(p, r);
        }
    }
}