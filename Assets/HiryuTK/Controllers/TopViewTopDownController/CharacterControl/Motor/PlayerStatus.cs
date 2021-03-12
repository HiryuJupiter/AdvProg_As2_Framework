using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class PlayerStatus
    {
        //Stats
        public int health;
        public int maxHealth;

        //Move
        public Vector2 currentVelocity;
        public float hurtDuration;

        //Hurt state
        [HideInInspector] public Vector2 hurtDriftDirection;

        public PlayerStatus(int maxHealth)
        {
            health = this.maxHealth = maxHealth;
        }

        public void CachePreviousStatus()
        {
        }

        public void SetAttackAnimationTimer(MonoBehaviour mono, float duration)
        {
            mono.StartCoroutine(TickAttackAnimationTimer(duration));
        }

        private IEnumerator TickAttackAnimationTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
        }

    }
}