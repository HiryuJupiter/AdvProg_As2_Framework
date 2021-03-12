﻿using UnityEngine;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{

    public abstract class MotorStateBase
    {
        protected TopDownControllerSetting settings;
        protected PlayerTopDown3DController player;
        protected PlayerFeedbacks feedback;
        protected PlayerStatus motorStatus;

        protected List<ModuleBase> modules = new List<ModuleBase>();

        public MotorStateBase(PlayerTopDown3DController player, PlayerFeedbacks feedback)
        {
            this.player     = player;
            this.feedback   = feedback;
            motorStatus     = player.Status;
            settings        = TopDownControllerSetting.Instance;
        }

        public virtual void StateEntry()
        {
            foreach (var m in modules)
                m.ModuleEntry();
        }

        public virtual void TickUpdate()
        {
            foreach (var m in modules)
                m.TickUpdate();
        }

        public virtual void TickFixedUpdate()
        {
            foreach (var m in modules)
                m.TickFixedUpdate();
        }

        public virtual void StateExit()
        {
            foreach (var m in modules)
                m.ModuleExit();
        }
    }
}