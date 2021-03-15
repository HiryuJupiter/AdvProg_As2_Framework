using UnityEngine;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{
    public class ShipState_Normal : ShipStateBase
    {
        public ShipState_Normal(PlayerTopDown3DController player, PlayerFeedbacks feedback) : base(player, feedback)
        {
            modules = new List<ModuleBase>()
            {
                new Module_Move(player, feedback),
                new Module_Rotation(player, feedback),
                new Module_BasicAttack(player, feedback),
                new Module_Mining(player, feedback),
            };
        }

    }
}
