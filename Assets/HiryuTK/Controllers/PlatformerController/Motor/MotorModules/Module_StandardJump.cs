﻿using UnityEngine;
using UnityEngine.UIElements;

namespace HiryuTK.Platformer2D
{
    /*=== TERMINOLOGIES ===
     * COYOTE TIME: After walking off a platform, the player has a brief moment where they can still jump.
     * JUMP QUEUE TIMER: The player can cache the jump command for a brief moment while midair. If it is cached right before landing, the player will automatically jump after landing. 
     */


    public class Module_StandardJump : ModuleBase
    {
        const float MaxJumpQueueDuration = 0.05f;

        public Module_StandardJump(Player2DMotor motor, Player2DFeedbacks feedback) : base(motor, feedback)
        { }

        #region Public 
        public override void ModuleEntry()
        {
            base.ModuleEntry();
            if (motorStatus.jumpQueueTimer > 0f)
            {
                OnJumpBtnDown();
            }
        }

        public override void TickUpdate()
        {
            TickTimers();



            if (GameInput.JumpBtnDown) // && !isJumping for onGround
            {
                if (motorStatus.canJump)
                {
                    OnJumpBtnDown();
                }
                //else
                //{
                //    motorStatus.jumpQueueTimer = MaxJumpQueueDuration;
                //}
            }

            if (GameInput.JumpBtnUp)
            {
                OnJumpBtnUp();
            }
        }

        public override void TickFixedUpdate()
        {
            base.TickFixedUpdate();
            CheckIfJustWalkeOffPlatform();

            if (motorStatus.justLanded)
            {
                motorStatus.isJumping = false;
                motorStatus.coyoteTimer = -1f;
            }
        }

        //public override void TickFixedUpdate()
        //{
        //    base.TickFixedUpdate();
        //    CheckIfJustWalkeOffPlatform();

        //    if (motorStatus.justLanded)
        //    {
        //        //If player just landed and has a jump queued up.
        //        //if (motorStatus.jumpQueueTimer > 0f)
        //        //{
        //        //    OnJumpBtnDown();
        //        //}
        //        //else
        //        {
        //            motorStatus.isJumping = false;
        //            motorStatus.coyoteTimer = -1f;
        //        }
        //    }
        //}
        #endregion

        void OnJumpBtnDown()
        {
            motorStatus.isJumping = true;
            motorStatus.jumpQueueTimer = -1f;
            motorStatus.coyoteTimer = -1f;

            motorStatus.currentVelocity.y = settings.MaxJumpForce;
        }

        void OnJumpBtnUp()
        {
            if (motorStatus.currentVelocity.y > settings.MinJumpForce)
            {
                motorStatus.currentVelocity.y = settings.MinJumpForce;
            }
        }

        void TickTimers()
        {
            if (motorStatus.coyoteTimer > 0f)
            {
                motorStatus.coyoteTimer -= Time.deltaTime;
            }

            if (motorStatus.jumpQueueTimer > 0f)
            {
                motorStatus.jumpQueueTimer -= Time.deltaTime;
            }
        }

        void CheckIfJustWalkeOffPlatform()
        {
            if (!motorStatus.isOnGround && motorStatus.isOnGroundPrevious && !motorStatus.isJumping)
            {
                motorStatus.coyoteTimer = settings.MaxCoyoteDuration;
            }
        }
    }
}

//public override void TickUpdate()
//{
//    if (status.currentVelocity.y > 0)
//    {
//        status.currentVelocity.y = 0;
//    }
//}
