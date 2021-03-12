using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{

    public abstract class ModuleBase
    {
        protected TopDownControllerSetting settings;
        protected PlayerTopDown3DController player;
        protected PlayerStatus status;
        protected PlayerFeedbacks feedback;


        public ModuleBase(PlayerTopDown3DController player, PlayerFeedbacks feedback)
        {
            this.player = player;
            this.feedback = feedback;
            status = player.Status;

            settings = TopDownControllerSetting.Instance;
        }

        public virtual void ModuleEntry() { }
        public virtual void TickFixedUpdate() { }
        public virtual void TickUpdate() { }
        public virtual void ModuleExit() { }
    }
}