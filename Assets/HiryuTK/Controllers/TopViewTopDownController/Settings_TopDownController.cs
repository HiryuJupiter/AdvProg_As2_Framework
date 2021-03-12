using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-90000000)]
    public class Settings_TopDownController : MonoBehaviour
    {
        public static Settings_TopDownController Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int playerHealth;
        public LayerMask PlayerMaxHealth => playerHealth;

        [Header("Abilities")]
        [SerializeField] private float cd_BasicAttack = 1f;
        [SerializeField] private float cd_Mining = 0.5f;
        public float CD_BasicAttack => cd_BasicAttack;
        public float CD_Mining => cd_Mining;

        [Header("Layers")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;

        [Header("Player Movement")]
        [SerializeField] private float moveSpeed = 1000f;
        [SerializeField] private float accelerationSpeed = 20f;
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

        public float ScreenBound_Top { get; private set; }
        public float ScreenBound_Bot { get; private set; }
        public float ScreenBound_Left { get; private set; }
        public float ScreenBound_Right { get; private set; }

        //Cache for spawn point calculation
        private float[] xSubPoints;
        private float[] zSubPoints;

        private void Awake()
        {
            Instance = this;

            //The camera is an ortho camera facing down 
            Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0f));
            Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

            ScreenBound_Left = lowerLeft.x;
            ScreenBound_Right = upperRight.x;
            ScreenBound_Top = upperRight.z;
            ScreenBound_Bot = lowerLeft.z;

            //Initialize the subdivision points in the middle of the zone.
            int xDivisions = 4;
            float xdivisionDist = (ScreenBound_Right - ScreenBound_Left) / xDivisions;
            xSubPoints = new float[xDivisions - 1];
            for (int i = 0; i < xDivisions - 1; i++)
            {
                xSubPoints[i] = ScreenBound_Left + xdivisionDist * (1 + i);
            }

            int zDivisions = 3;
            float zdivisionDist = (ScreenBound_Top - ScreenBound_Bot) / zDivisions;
            zSubPoints = new float[zDivisions - 1];
            for (int i = 0; i < zDivisions - 1; i++)
            {
                zSubPoints[i] = ScreenBound_Bot + zdivisionDist * (1 + i);
            }

            //for (int x = 0; x < xSubPoints.Length; x++)
            //{
            //    for (int z = 0; z < zSubPoints.Length; z++)
            //    {
            //        Vector3 p = new Vector3(xSubPoints[x], 0f, zSubPoints[z]);
            //        Debug.Log(p);
            //        Debug.DrawLine(p, p + Vector3.up, Color.magenta, 10f);
            //    }
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Vector3 p = RandomSpawnPointXZ();
            //    Quaternion r = RandomSpawnRotationXZ(p);
            //    Debug.DrawLine(p, p + Vector3.up, Color.cyan, 40f);
            //}
        }

        public static bool IsTargetOnPlayerLayer(GameObject go) => Instance.PlayerLayer == (Instance.PlayerLayer | 1 << go.layer);
        public static bool IsTargetOnEnemyLayer(GameObject go) => Instance.EnemyLayer == (Instance.EnemyLayer | 1 << go.layer);
        public static bool IsTargetOnGroundLayer(GameObject go) => Instance.GroundLayer == (Instance.GroundLayer | 1 << go.layer);

        public Vector3 RandomSpawnPointXZ ()
        {
            if (RandomBool)
            {
                //Spawn top and bottom
                float x = Random.Range(ScreenBound_Left, ScreenBound_Right);
                float z = RandomBool ? ScreenBound_Bot - 5f : ScreenBound_Top + 5f;
                return new Vector3(x, 0f, z);
            }
            else
            {
                //Spawn left and right edge
                float z = Random.Range(ScreenBound_Top, ScreenBound_Bot);
                float x = RandomBool ? ScreenBound_Left - 5f : ScreenBound_Right + 5f;
                return new Vector3(x, 0f, z);
            }
        }

        public Quaternion RandomSpawnRotationXZ (Vector3 spawnPoint)
        {
            Vector3 aim = new Vector3(
                xSubPoints[Random.Range(0, xSubPoints.Length)], 
                0f, 
                zSubPoints[Random.Range(0, zSubPoints.Length)]);
            //Debug.DrawRay(spawnPoint, aim - spawnPoint, Color.yellow, 10f);

            return Quaternion.LookRotation(aim - spawnPoint, Vector3.up);
        }

        bool RandomBool => Random.Range(0, 2) == 0;
    }
}