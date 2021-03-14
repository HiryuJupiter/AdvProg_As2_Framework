﻿using UnityEngine;
using System.Collections;

namespace HiryuTK.ObjectPool
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager instance;

        [Header("Pf")]
        [SerializeField] private PoolObject pf_playerBullet;
        [SerializeField] private PoolObject pf_enemyShip;
        [SerializeField] private PoolObject pf_Asteroid;
        public Pool playerBullet { get; private set; }
        public Pool enemyShip { get; private set; }
        public Pool asteroids { get; private set; }


        void Awake()
        {
            instance = this;

            playerBullet = new Pool(pf_playerBullet, transform);
            enemyShip = new Pool(pf_enemyShip, transform);
            asteroids = new Pool(pf_Asteroid, transform);
        }

        public PoolObject SpawnPlayerBullet(Vector3 p, Quaternion r) => playerBullet.Spawn(p, r);
        public PoolObject SpawnEnemyBullet(Vector3 p, Quaternion r) => enemyShip.Spawn(p, r);
        public PoolObject SpawnAsteroid(Vector3 p, Quaternion r) => asteroids.Spawn(p, r);
    }
}