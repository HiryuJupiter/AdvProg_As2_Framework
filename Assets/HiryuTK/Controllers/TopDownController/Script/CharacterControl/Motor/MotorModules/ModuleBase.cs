using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public abstract class ModuleBase
    {
        protected Settings_TopDownController settings;
        protected PlayerTopDown3DController player;
        protected PlayerStatus status;
        protected PlayerFeedbacks feedback;
        protected Transform transform;
        protected GameInput_TopDownController input;

        public ModuleBase(PlayerTopDown3DController player, PlayerFeedbacks feedback)
        {
            this.player = player;
            this.feedback = feedback;
            transform = player.transform;
            status = player.Status;

            input = GameInput_TopDownController.Instance;
            settings = Settings_TopDownController.Instance;
        }

        public virtual void ModuleEntry() { }
        public virtual void TickFixedUpdate() { }
        public virtual void TickUpdate() { }
        public virtual void ModuleExit() { }
    }
}