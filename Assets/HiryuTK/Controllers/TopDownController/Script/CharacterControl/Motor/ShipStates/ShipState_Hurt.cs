using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class ShipState_Hurt : ShipStateBase
    {
        public ShipState_Hurt(PlayerTopDown3DController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
        {
            modules = new List<ModuleBase>()
            {
                new Module_Rotation(player, feedbacks),
                new Module_HurtKnockBack(player, feedbacks),
            };
        }
    }
}