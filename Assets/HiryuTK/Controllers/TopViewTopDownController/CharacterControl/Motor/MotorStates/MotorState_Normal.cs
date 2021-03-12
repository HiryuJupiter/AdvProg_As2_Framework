using UnityEngine;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{
    public class MotorState_Normal : MotorStateBase
    {
        public MotorState_Normal(PlayerTopDown3DController player, PlayerFeedbacks feedback) : base(player, feedback)
        {
            modules = new List<ModuleBase>()
            {
                new Module_Move(player, feedback),
                new Module_Rotation(player, feedback),
            };
        }

    }
}
