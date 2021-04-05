using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    /// <summary>
    /// Holds various utility methods to make the other classes shorter
    /// </summary>
    public static class CombatUtil
    {
        /// <summary>
        /// Get corresponding portrait for P1
        /// </summary>
        public static string GetP1Portrait(CombatStance stance)
             => stance switch
            {
                CombatStance.Jump => CombatPortraits.playerPortrait_Jump,
                CombatStance.Duck => CombatPortraits.playerPortrait_Duck,
                CombatStance.Dead => CombatPortraits.GraveRIP,
                _ => CombatPortraits.playerPortrait_Idle
            };

        /// <summary>
        /// Get corresponding portrait for P2
        /// </summary>
        public static string GetP2ortrait(CombatStance stance)
            => stance switch
            {
                CombatStance.Jump => CombatPortraits.enemyPortrait_Jump,
                CombatStance.Duck => CombatPortraits.enemyPortrait_Duck,
                CombatStance.Dead => CombatPortraits.GraveRIP,
                _ => CombatPortraits.enemyPortrait_Idle
            };

        /// <summary>
        /// Check if target can take damage
        /// </summary>
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
