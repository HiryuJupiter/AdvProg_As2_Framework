using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-90000000)]
    public class Settings_TopDownController : MonoBehaviour
    {
        public static Settings_TopDownController Instance { get; private set; }

        [Header("Stats")]
        [SerializeField] private int playerHealth = 3;
        public LayerMask PlayerMaxHealth => playerHealth;

        [Header("Abilities")]
        [SerializeField] private float basicBullet_speed = 20f;
        [SerializeField] private float cd_BasicAttack = 1f;
        [SerializeField] private float cd_Mining = 0.1f;
        [SerializeField] private float   miningPower = 5f;
        [SerializeField] private float miningDistance = 50f;
        public float BasicBullet_speed => basicBullet_speed;
        public float CD_BasicAttack => cd_BasicAttack;
        public float CD_Mining => cd_Mining;
        public float MiningPower => miningPower;
        public float MiningDistance => miningDistance;

        [Header("Layers")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask enemyLayer;
        public LayerMask PlayerLayer => playerLayer;
        public LayerMask GroundLayer => groundLayer;
        public LayerMask EnemyLayer => enemyLayer;

        [Header("Player Movement")]
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float moveAcceleration = 1f;
        [SerializeField] private float rotationSpeed = 20f; //50f
        [SerializeField] private float rotationAccleration= 1f; //50f
        public float MoveSpeed => moveSpeed;
        public float MoveAcceleration => moveAcceleration;
        public float RotationSpeed => rotationSpeed;
        public float RotationAccleration => rotationAccleration;
        
        [Header("Enemy Movement")]
        [SerializeField] private float enemyMove = 2f;
        [SerializeField] private float enemyRotation = 100f;
        public float EnemyMove => enemyMove;
        public float EnemyRotation => enemyRotation;

        [Header("Asteroid Movement")]
        [SerializeField] private float asteroidMove =  15f;
        [SerializeField] private float asteroidRotation = 10f;
        public float AsteroidMove => asteroidMove;
        public float AsteroidRotation => asteroidRotation;

        [Header("Hurt State")]
        [SerializeField] private float hurtSlideSpeed = 20f; //50f
        [SerializeField] private float hurtDuration = 0.5f;
        public float HurtSlideSpeed => hurtSlideSpeed;
        public float HurtDuration => hurtDuration;

        public float ScreenBound_Top { get; private set; }
        public float ScreenBound_Bot { get; private set; }
        public float ScreenBound_Left { get; private set; }
        public float ScreenBound_Right { get; private set; }
        public float innerSideline_Top { get; private set; }
        public float innerSideline_Bot { get; private set; }
        public float innerSideline_Left { get; private set; }
        public float innerSideline_Right { get; private set; }
        public float outerSideline_Top { get; private set; }
        public float outerSideline_Bot { get; private set; }
        public float outerSideline_Left { get; private set; }
        public float outerSideline_Right { get; private set; }


        //Cache for spawn point calculation
        private float[] xSubPoints;
        private float[] ySubPoints;

        private void Awake()
        {
            Instance = this;

            Vector2 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f));
            Vector2 upperRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            ScreenBound_Top = upperRight.y;
            ScreenBound_Bot = lowerLeft.y;
            ScreenBound_Left = lowerLeft.x;
            ScreenBound_Right = upperRight.x;

            float offset = .4f;
            innerSideline_Top       = ScreenBound_Top   + offset;
            innerSideline_Bot       = ScreenBound_Bot   - offset;
            innerSideline_Left      = ScreenBound_Left  - offset;
            innerSideline_Right     = ScreenBound_Right + offset;

            float offsetSmall = 1f;
            outerSideline_Top       = innerSideline_Top     + offsetSmall;
            outerSideline_Bot       = innerSideline_Bot     - offsetSmall;
            outerSideline_Left      = innerSideline_Left    - offsetSmall;
            outerSideline_Right     = innerSideline_Right   + offsetSmall;

            Debug.DrawLine(new Vector2(outerSideline_Left, outerSideline_Top), new Vector2(outerSideline_Right, outerSideline_Top), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Left, outerSideline_Top), new Vector2(outerSideline_Left, outerSideline_Bot), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Right, outerSideline_Bot), new Vector2(outerSideline_Left, outerSideline_Bot), Color.red, 10f);
            Debug.DrawLine(new Vector2(outerSideline_Right, outerSideline_Bot), new Vector2(outerSideline_Right, outerSideline_Top), Color.red, 10f);

            Debug.DrawLine(new Vector2(innerSideline_Left, innerSideline_Top), new Vector2(innerSideline_Right, innerSideline_Top), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Left, innerSideline_Top), new Vector2(innerSideline_Left, innerSideline_Bot), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Right, innerSideline_Bot), new Vector2(innerSideline_Left, innerSideline_Bot), Color.white, 10f);
            Debug.DrawLine(new Vector2(innerSideline_Right, innerSideline_Bot), new Vector2(innerSideline_Right, innerSideline_Top), Color.white, 10f);

            //Initialize the subdivision points in the middle of the zone.
            int xDivisions = 4;
            float xdivisionDist = (ScreenBound_Right - ScreenBound_Left) / xDivisions;
            xSubPoints = new float[xDivisions - 1];
            for (int i = 0; i < xDivisions - 1; i++)
            {
                xSubPoints[i] = ScreenBound_Left + xdivisionDist * (1 + i);
            }

            int yDivisions = 4;
            float ydivisionDist = (ScreenBound_Top - ScreenBound_Bot) / yDivisions;
            ySubPoints = new float[yDivisions - 1];
            for (int i = 0; i < yDivisions - 1; i++)
            {
                ySubPoints[i] = ScreenBound_Bot + ydivisionDist * (1 + i);
            }

            //for (int x = 0; x < xSubPoints.Length; x++)
            //{
            //    for (int z = 0; z < ySubPoints.Length; z++)
            //    {
            //        Vector2 p = new Vector2(xSubPoints[x], ySubPoints[z]);
            //        Debug.Log(p);
            //        Debug.DrawLine(p, p + Vector2.right, Color.magenta, 10f);
            //    }
            //}

            //for (int i = 0; i < 50; i++)
            //{
            //    Vector2 p = RandomSpawnPoint();
            //    Quaternion r = RandomSpawnRotation(p);
            //    Debug.DrawLine(p, p + Vector2.right, Color.cyan, 40f);
            //}
        }

        //public bool IsTargetOnPlayerLayer(GameObject go) => PlayerLayer == (PlayerLayer | 1 << go.layer);
        //public bool IsTargetOnEnemyLayer(GameObject go) => EnemyLayer == (EnemyLayer | 1 << go.layer);
        //public bool IsTargetOnGroundLayer(GameObject go) => GroundLayer == (GroundLayer | 1 << go.layer);
        public bool IsTargetPlayer(GameObject go) => go.tag == "Player";
        public bool IsTargetEnemy(GameObject go) => go.tag == "Enemy";
        public bool IsTargetGround(GameObject go) => go.tag == "Ground";

        public Vector2 RandomSpawnPoint ()
        {
            if (RandomBool)
            {
                //Spawn top and bottom
                float x = Random.Range(ScreenBound_Left, ScreenBound_Right);
                float y = RandomBool ? innerSideline_Bot : innerSideline_Top;
                return new Vector2(x, y);
            }
            else
            {
                //Spawn left and right edge
                float y = Random.Range(ScreenBound_Top, ScreenBound_Bot);
                float x = RandomBool ? innerSideline_Left : innerSideline_Right;
                return new Vector2(x, y);
            }
        }

        public Quaternion RandomSpawnRotation (Vector2 spawnPoint)
        {
            Vector2 aim = new Vector2(
                xSubPoints[Random.Range(0, xSubPoints.Length)], 
                ySubPoints[Random.Range(0, ySubPoints.Length)]);
            //Debug.DrawRay(spawnPoint, aim - spawnPoint, Color.yellow, 10f);

            return Quaternion.LookRotation(Vector3.forward, aim - spawnPoint);
        }

        public bool CanBeScreenWarped (Vector2 position, out Vector2 warpPosition)
        {
            warpPosition = position;
            //Warp Left >>> Right
            if (position.x < innerSideline_Left)
            {
                warpPosition.x = innerSideline_Right - .01F;
                return true;
            }
            //Warp Right >>> Left
            if (position.x > innerSideline_Right)
            {
                warpPosition.x = innerSideline_Left + .01F;
                return true;
            }

            //Warp Bot >>> Top
            if (position.y < innerSideline_Bot)
            {
                warpPosition.y = innerSideline_Top - .01F;
                return true;
            }

            //Warp Top >>> Bot
            if (position.y > innerSideline_Top)
            {
                warpPosition.y = innerSideline_Bot + .01F;
                return true;
            }
            return false;
        }

        public bool IsOutOfBounds(Vector2 position)
        {
            //Warp Left >>> Right
            if (position.x < outerSideline_Left || position.x > outerSideline_Right ||
                position.y < outerSideline_Bot  || position.y > outerSideline_Top)
            {
                return true;
            }
            return false;
        }

        bool RandomBool => Random.Range(0, 2) == 0;

        void DebugVisualization ()
        {
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
    }
}


/*
 using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-90000000)]
    public class Settings_TopDownController : MonoBehaviour
    {
        //Bound offsets
        const int OuterOffset = 10;
        const int InnerOffset = 5;

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

            int zDivisions = 4;
            float zdivisionDist = (ScreenBound_Top - ScreenBound_Bot) / zDivisions;
            zSubPoints = new float[zDivisions - 1];
            for (int i = 0; i < zDivisions - 1; i++)
            {
                zSubPoints[i] = ScreenBound_Bot + zdivisionDist * (1 + i);
            }
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

        public bool CanBeScreenWarped (Vector3 position, out Vector3 warpPosition)
        {
            warpPosition = position;
            //Left >>> Right
            if (position.x < ScreenBound_Left - 5f)
            {
                return true;
            }
            //Right >>> Left
            if (position.x < ScreenBound_Left - 5f)
            {
                return true;
            }
            //Top >>> Bot
            if (position.x < ScreenBound_Left - 5f)
            {
                return true;
            }
            //Bot >>> Top
            if (position.x < ScreenBound_Left - 5f)
            {
                return true;
            }
            return false;
        }



        bool RandomBool => Random.Range(0, 2) == 0;




        void DebugVisualization ()
        {
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
    }
}
 
 */