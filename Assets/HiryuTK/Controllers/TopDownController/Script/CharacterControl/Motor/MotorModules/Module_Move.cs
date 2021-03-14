using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class Module_Move : ModuleBase
    {
        //Cache
        float boundLeft;
        float boundRight;
        float boundTop;
        float boundBot;

        public Module_Move(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
        {
            //Cache
            boundLeft = settings.ScreenBound_Left + 0.4f;
            boundRight = settings.ScreenBound_Right - 0.4f;
            boundTop = settings.ScreenBound_Top - 0.4f;
            boundBot = settings.ScreenBound_Bot + 0.4f;
        }

        #region Public methods
        public override void ModuleEntry()
        {
            base.ModuleEntry();
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
        }

        public override void TickFixedUpdate()
        {
            float drive = Mathf.Clamp(GameInput_TopDownController.MoveY, 0f, 1f);

            status.velocity.y = Mathf.Lerp(status.velocity.y, drive * settings.MoveSpeed, 
                Time.deltaTime * settings.MoveAcceleration);
        }
        #endregion

        #region Helpers
        bool CanMoveLeft => transform.position.x > boundLeft;
        bool CanMoveRight => transform.position.x < boundRight;
        bool CanMoveUp => transform.position.y < boundTop;
        bool CanMoveDown => transform.position.y > boundBot;
        bool MovingWithinXBounds => (status.MovingLeft && CanMoveLeft) || (status.MovingRight && CanMoveRight);
        bool MovingWithinYBounds => (status.MovingUp && CanMoveUp) || (status.MovingDown && CanMoveDown);
        bool HasHorizontalInput => (GameInput_TopDownController.MoveX > 0.1f || 
            GameInput_TopDownController.MoveX < -0.1f);
        bool HasVerticalInput => (GameInput_TopDownController.MoveY > 0.1f || 
            GameInput_TopDownController.MoveY < -0.1f);
        bool HasMovementInput => HasHorizontalInput || HasVerticalInput;
        #endregion
    }
}