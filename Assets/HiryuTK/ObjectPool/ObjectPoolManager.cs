using UnityEngine;
using System.Collections;

namespace HiryuTK.ObjectPool
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager instance;

        [Header("Pfx")]
        [SerializeField] PoolObject pf_basicBullet;
        public Pool basicBullet;

        [Header("Environment")]
        [SerializeField] PoolObject pf_Asteroid;
        public Pool asteroids;

        void Awake()
        {
            instance = this;

            basicBullet = new Pool(pf_basicBullet, transform);
            asteroids = new Pool(pf_Asteroid, transform);
        }

        public PoolObject SpawnBasicBullet(Vector3 p, Quaternion r) => basicBullet.Spawn(p, r);
    }
}