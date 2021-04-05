using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public static class CombatUtil
    {
        public static string GetP1Portrait(CombatStance stance)
             => stance switch
            {
                CombatStance.Jump => CombatPortraits.playerPortrait_Jump,
                CombatStance.Duck => CombatPortraits.playerPortrait_Duck,
                CombatStance.Dead => CombatPortraits.GraveRIP,
                _ => CombatPortraits.playerPortrait_Idle
            };


        public static string GetP2ortrait(CombatStance stance)
            => stance switch
            {
                CombatStance.Jump => CombatPortraits.enemyPortrait_Jump,
                CombatStance.Duck => CombatPortraits.enemyPortrait_Duck,
                CombatStance.Dead => CombatPortraits.GraveRIP,
                _ => CombatPortraits.enemyPortrait_Idle
            };

        public static bool CanTargetTakeDamage(CombatStance targetStance, CombatStance bulletStance)
        {
            return bulletStance switch
            {
                CombatStance.Jump => (targetStance == CombatStance.Jump) || (targetStance == CombatStance.Stand),
                CombatStance.Duck => (targetStance == CombatStance.Duck) || (targetStance == CombatStance.Stand),
                CombatStance.Stand => targetStance == CombatStance.Stand,
                _ => false
            };
        }
    }

}
