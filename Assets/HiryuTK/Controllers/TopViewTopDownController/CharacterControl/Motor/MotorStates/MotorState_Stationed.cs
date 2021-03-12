using UnityEngine;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{
    public class MotorState_Stationed : MotorStateBase
    {
        public MotorState_Stationed(PlayerTopDown3DController player, PlayerFeedbacks feedback) : base(player, feedback)
        {
            modules = new List<ModuleBase>()
            {
                //new Module_Move(player, feedback),
                //new Module_BasicAttack(player, feedback),
            };
        }

    }
}
