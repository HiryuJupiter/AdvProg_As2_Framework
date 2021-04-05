using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public abstract class AbilityBase
    {
        public string abilityName;

        protected readonly int baseDamage;
        protected readonly StatusEffectBase statusAgainstEnemy;
        protected readonly int statusToEnemyDuration;

        protected readonly int selfHeal;
        protected readonly StatusEffectBase statusApplyToSelf;
        protected readonly int statusToSelfDuration;

        public AbilityBase()
        {

        }

        public virtual void Reset()
        {
            
        }
    }
}