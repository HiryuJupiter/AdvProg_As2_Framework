using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-90000000)]
    public class TopDownControllerSetting : MonoBehaviour
    {
        public static TopDownControllerSetting Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int playerHealth;
        public LayerMask PlayerMaxHealth => playerHealth;

        [Header("Layers")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;

        [Header("Player Movement")]
        [SerializeField] private float moveSpeed = 6;
        [SerializeField] private float accelerationSpeed = 6;
        [SerializeField] private float steerSpeed = 1f; //50f
        public float MoveSpeed => moveSpeed;
        public float AccelerationSpeed => accelerationSpeed;
        public float SteerSpeed => steerSpeed;

        [Header("Hurt State")]
        [SerializeField] private float hurtSlideSpeed = 20f; //50f
        [SerializeField] private Vector3 hurtDirection = new Vector3(0f, 25f, 20f);
        [SerializeField] private float hurtDuration = 0.5f;
        public float HurtSlideSpeed => hurtSlideSpeed;
        public Vector2 HurtDirection => hurtDirection;
        public float HurtDuration => hurtDuration;

        private void Awake()
        {
            //Singleton
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public static bool IsTargetOnPlayerLayer(GameObject go) => Instance.PlayerLayer == (Instance.PlayerLayer | 1 << go.layer);
        public static bool IsTargetOnEnemyLayer(GameObject go) => Instance.EnemyLayer == (Instance.EnemyLayer | 1 << go.layer);
        public static bool IsTargetOnGroundLayer(GameObject go) => Instance.GroundLayer == (Instance.GroundLayer | 1 << go.layer);
    }
}