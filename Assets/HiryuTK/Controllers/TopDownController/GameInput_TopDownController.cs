using UnityEngine;

namespace HiryuTK.TopDownController
{
    [DefaultExecutionOrder(-9000000)]
    public class GameInput_TopDownController : MonoBehaviour
    {
        public static GameInput_TopDownController Instance;
        public static float MoveX { get; set; }
        public static float MoveY { get; set; }
        public static bool JumpBtnDown { get; set; }
        public static bool JumpBtn { get; set; }
        public static bool JumpBtnUp { get; set; }
        public static bool PressedLeft => MoveX < -0.1f;
        public static bool PressedRight => MoveX > 0.1f;
        public static bool PressedDown => MoveY < -0.1f;
        public static bool PressedUp => MoveY < 0.1f;
        public static bool IsMoving => MoveX != 0 || MoveY != 0;
        public static float MouseX;
        public static float MouseY;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            DirectionInputUpdate();
            ActionInputUpdate();
        }

        private void DirectionInputUpdate()
        {
            //LEFT - RIGHT
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                MoveX = -1f;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                MoveX = 1f;
            }
            else
            {
                MoveX = 0f;
            }

            //UP - DOWN
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                MoveY = 1f;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                MoveY = -1f;
            }
            else
            {
                MoveY = 0f;
            }

            //Mouse look
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
        }

        private void ActionInputUpdate()
        {
            JumpBtnDown = Input.GetKeyDown(KeyCode.Space);
            JumpBtn = Input.GetKey(KeyCode.Space);
            JumpBtnUp = Input.GetKeyUp(KeyCode.Space);
        }

        //void OnGUI()
        //{
        //    GUI.Label(new Rect(20, 20, 500, 20), "UP " + KeyScheme.Up);
        //    GUI.Label(new Rect(20, 40, 500, 20), "DOWN " + KeyScheme.Down);
        //    GUI.Label(new Rect(20, 60, 500, 20), "Left " + KeyScheme.Left);
        //    GUI.Label(new Rect(20, 80, 500, 20), "Right " + KeyScheme.Right);
        //    GUI.Label(new Rect(20, 110, 500, 20), "MoveX " + MoveX);
        //    GUI.Label(new Rect(20, 130, 500, 20), "MoveY " + MoveY);
        //}
    }
}
