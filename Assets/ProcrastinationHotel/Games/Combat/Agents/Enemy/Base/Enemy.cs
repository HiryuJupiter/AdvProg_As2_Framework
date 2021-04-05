using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public abstract class Enemy
    {
        public string name = "Unamed";

        public string portrait;
        public CharacterStatus status;
        public List<AbilityBase> abilities { get; protected set; } = new List<AbilityBase>();

        public Enemy()
        {
            status = new CharacterStatus();
        }

        public void ApplyAbility()
        {

        }
    }
}