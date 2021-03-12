using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-100)]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerFeedbacks))]
    public class PlayerTopDown3DController : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform shootPoint;
        [SerializeField] private LineRenderer lineRenderer;

        //Class and components
        private PlayerFeedbacks feedback;
        private Settings_TopDownController setting;
        private UIManager uiM;

        //States
        private MotorStates currentStateType;
        private MotorStateBase currentStateClass;
        private Dictionary<MotorStates, MotorStateBase> stateClassLookup;

        public PlayerStatus Status { get; private set; }
        public Rigidbody Rb { get; private set; }
        public Transform ShootPoint => shootPoint;
        public LineRenderer LineRenderer => lineRenderer;
        #endregion

        #region MonoBehiavor
        private void Awake()
        {
            //Reference
            Rb = GetComponent<Rigidbody>();
            feedback = GetComponent<PlayerFeedbacks>();
            setting = Settings_TopDownController.Instance;

            //Initialize
            Status = new PlayerStatus(setting.PlayerMaxHealth);
            stateClassLookup = new Dictionary<MotorStates, MotorStateBase>
            {
                {MotorStates.Stationed,     new MotorState_Stationed(this, feedback)},
                {MotorStates.Normal,        new MotorState_Normal(this, feedback)},
                {MotorStates.Hurt,          new MotorState_Hurt(this, feedback)},
            };

            currentStateType = MotorStates.Normal;
            currentStateClass = stateClassLookup[currentStateType];
            currentStateClass.StateEntry();
        }

        private void Start()
        {
            uiM = UIManager.Instance;
        }

        private void Update()
        {
            currentStateClass?.TickUpdate();
        }

        private void FixedUpdate()
        {
            Status.CachePreviousStatus();
            CalculateCurrentStatus();

            currentStateClass?.TickFixedUpdate();

            ExecuteRigidbodyVelocity();
        }
        #endregion

        #region Public 
        public void DamagePlayer(Vector2 enemyPos, int damage)
        {
            if (currentStateType != MotorStates.Hurt)
            {
                Status.health -= damage;
                if (Status.health <= 0)
                {
                    Status.health = 0;
                    uiM.SetDeathScreenVisibility(true);
                }
                else
                {
                    SwitchToNewState(MotorStates.Hurt);
                }
                uiM.SetHealth(Status.health / Status.maxHealth);
            }
        }

        public void RotateCharacter(float amount)
        {
            //Rotate the character by multiplying rotation amount to current quaternion
            Quaternion q = Quaternion.AngleAxis(amount, Vector3.up);
            var targetRot = Rb.rotation * q;
            Rb.rotation = targetRot;
        }

        public void SwitchToNewState(MotorStates newStateType)
        {
            if (currentStateType != newStateType)
            {
                currentStateType = newStateType;

                currentStateClass.StateExit();
                currentStateClass = stateClassLookup[newStateType];
                currentStateClass.StateEntry();
            }
        }
        #endregion

        #region Private
        private void ExecuteRigidbodyVelocity()
        {
            Rb.velocity = Status.velocity;
            //Rb.velocity = transform.TransformDirection(Status.velocity);
            //Rb.velocity = cameraController.NonTiltedRotationTowardsPlayer * Status.currentVelocity;
        }
        #endregion

        #region Pre-calculations
        private void CalculateCurrentStatus()
        {
            //Status.isOnGround = Raycaster.OnGrounDcheck();
        }
        #endregion

        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 500, 20), "Current State   : " + currentStateType);

            GUI.Label(new Rect(20, 60, 290, 20), "=== MOVE === ");
            GUI.Label(new Rect(20, 80, 290, 20), "health: " + Status.health);
            GUI.Label(new Rect(20, 100, 290, 20), "currentVelocity: " + Status.velocity);
            GUI.Label(new Rect(20, 120, 290, 20), "rb velocity    : " + Rb.velocity);
            GUI.Label(new Rect(20, 160, 290, 20), "hurtTimer      : " + Status.hurtDuration);
            GUI.Label(new Rect(20, 180, 290, 20), "hurtDriftDirection: " + Status.hurtDriftDirection);
            //GUI.Label(new Rect(200, 0, 290, 20), "=== JUMPING === ");
            //GUI.Label(new Rect(200, 20, 290, 20), );
            //GUI.Label(new Rect(200, 40, 290, 20), );
            //GUI.Label(new Rect(200, 60, 290, 20), );
            //GUI.Label(new Rect(200, 80, 290, 20), );

            GUI.Label(new Rect(400, 0, 290, 20), "=== INPUT === ");
            GUI.Label(new Rect(400, 20, 290, 20), "MoveX: " + GameInput_TopDownController.MoveX);
            GUI.Label(new Rect(400, 40, 290, 20), "MoveZ: " + GameInput_TopDownController.MoveZ);
            //GUI.Label(new Rect(300, 120,		290, 20), "testLocation: " + testLocation);
        }
    }
}