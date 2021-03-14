using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class TopDown_EnvironmentSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnIntervalMin = 2f;
        [SerializeField] private float spawnIntervalMax = 10f;
        //[SerializeField] private GameObject prefab = null;

        private Settings_TopDownController settings;
        private ObjectPoolManager_TopDown poolM;

        private float timer = 0f;
        private float speedUpMod = 1f;
        private float speedUpSpeed = 0.01f;


        private void Start()
        {
            settings    = Settings_TopDownController.Instance;
            poolM       = ObjectPoolManager_TopDown.instance;
            Spawn(5);
        }

        private void Spawn(int count)
        {
            StartCoroutine(DoSpawn(count));
        }

        private IEnumerator DoSpawn(int count)
        {
            
            //RefreshTimer();

            while (count > 0)
            {
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    //if (Random.Range(0, 2) == 0) 
                    //    SpawnAsteroid();
                    //else
                    //    SpawnEnemyShip();
                    SpawnEnemyShip();
                    count++;
                    RefreshTimer();
                }
                yield return null;
            }
        }

        void RefreshTimer()
        {
            timer = Random.Range(spawnIntervalMin, spawnIntervalMax);
            while (speedUpMod> 0.1f)
            {
                speedUpMod -= speedUpSpeed;
            }
            timer *= speedUpMod;
        }

        private void SpawnAsteroid()
        {
            Vector3 p = settings.RandomSpawnPoint();
            Quaternion r = settings.RandomSpawnRotation(p);
            Debug.DrawRay(p, r * Vector3.up * 10f, Color.red, 10f);
            //Instantiate(prefab, p, r);
            poolM.SpawnAsteroid(p, r);
        }

        private void SpawnEnemyShip ()
        {
            Vector3 p = settings.RandomSpawnPoint();
            Quaternion r = settings.RandomSpawnRotation(p);
            Debug.DrawRay(p, r * Vector3.up * 10f, Color.red, 10f);
            //Instantiate(prefab, p, r);
            poolM.SpawnEnemyShip(p, r);
        }
    }
}