using System.Collections;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public abstract class StatusEffectBase
    {
        public abstract void ApplyEffectToCharacterStatus( CharacterStatus character);
        public abstract void ApplyEffectToAbility( AbilityBase ability);
    }
}