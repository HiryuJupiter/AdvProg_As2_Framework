using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class TopDown_AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private float spawnIntervalMin = 0.1f;
        [SerializeField] private float spawnIntervalMax = 0.5f;
        [SerializeField] private GameObject prefab = null;

        Settings_TopDownController settings;

        void Start()
        {
            settings    = Settings_TopDownController.Instance;
            Spawn(5);
        }

        void Spawn(int count)
        {
            StartCoroutine(DoSpawn(count));
        }

        IEnumerator DoSpawn(int count)
        {
            float timer;
            void RefreshTimer() => timer = Random.Range(spawnIntervalMin, spawnIntervalMax);
            RefreshTimer();

            while (count > 0)
            {
                if (timer > 0f)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    SpawnAsteroid();
                    RefreshTimer();
                }
                yield return null;
            }
        }

        void SpawnAsteroid()
        {
            Vector3 p = settings.RandomSpawnPointXZ();
            Quaternion r = settings.RandomSpawnRotationXZ(p);
            Debug.DrawRay(p, r * Vector3.forward * 10f, Color.red, 10f);
            Instantiate(prefab, p, r);
        }
    }
}