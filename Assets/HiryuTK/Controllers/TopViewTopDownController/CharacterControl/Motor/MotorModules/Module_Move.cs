using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class Module_Move : ModuleBase
    {
        const float RotationSpeed = 2f;

        private float moveXSmoothDampVelocity;
        private float moveZSmoothDampVelocity;
        private bool crawling;

        //Ctor
        public Module_Move(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback)
        { }

        #region Public methods
        public override void ModuleEntry()
        {
            base.ModuleEntry();
        }

        public override void TickUpdate()
        {
            base.TickUpdate();
            //CharacterRotationUpdate();
        }

        public override void TickFixedUpdate()
        {
            //Modify x-velocity
            status.currentVelocity.x = Mathf.SmoothDamp(status.currentVelocity.x, GameInput.MoveX * 
                settings.MoveSpeed, ref moveXSmoothDampVelocity, settings.AccelerationSpeed * Time.deltaTime);
            status.currentVelocity.y = Mathf.SmoothDamp(status.currentVelocity.y, GameInput.MoveZ * 
                settings.MoveSpeed, ref moveZSmoothDampVelocity, settings.AccelerationSpeed * Time.deltaTime);
        }

        public override void ModuleExit()
        {
            base.ModuleExit();
            crawling = false;
        }
        #endregion
    }
}