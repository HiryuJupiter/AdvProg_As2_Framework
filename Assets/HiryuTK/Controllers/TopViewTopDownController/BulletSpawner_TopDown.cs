using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-10)]
    public class BulletSpawner_TopDown : MonoBehaviour
    {
        public static BulletSpawner_TopDown Instance { get; private set; }

        [SerializeField] private GameObject bullet;

        public GameObject Bullet => bullet;

        void Awake()
        {
            Instance = this;
        }

    }
}