using UnityEngine;
using System.Collections;

namespace HiryuTK.Core
{
    [DefaultExecutionOrder(-90000000)]
    public class Settings : MonoBehaviour
    {
        public static Settings Instance { get; private set; }

        [Header("Layers")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;

        [Header("Player Movement")]
        [SerializeField] private float steerSpeedGround = 1f; //50f
        [SerializeField] private float steerSpeedAir = 5f; //50f
        [SerializeField] private float playeRunSpeed = 15;
        [SerializeField] private float playerWalkSpeed = 10;
        [SerializeField] private float playerCrouchSpeed = 6;
        public float SteerSpeedGround => steerSpeedGround;
        public float SteerSpeedAir => steerSpeedAir;
        public float PlayerRunSpeed => playeRunSpeed;
        public float PlayerMoveSpeed => playerWalkSpeed;
        public float PlayerCrawlSpeed => playerCrouchSpeed;

        [Header("Normal Jump")]
        [SerializeField] private float minJumpForce = 12f;
        [SerializeField] private float maxJumpForce = 22f;
        [SerializeField] private float maxCoyoteDuration = 0.25f;
        public float MinJumpForce => minJumpForce;
        public float MaxJumpForce => maxJumpForce;
        public float MaxCoyoteDuration => maxCoyoteDuration;

        [Header("Hurt State")]
        [Range(10f, 50f)] [SerializeField] private float hurtSlideSpeed = 20f; //50f

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