using UnityEngine;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class PlayerStatus
    {
        //Stats
        public int health;
        public int maxHealth;
        public int money;

        //Move
        public Vector3 velocity;

        //Hurt
        public float hurtDuration;

        //Hurt state
        [HideInInspector] public Vector3 hurtDriftDirection;

        //Helpers
        public bool MovingLeft => velocity.x < 0f;
        public bool MovingRight => velocity.x > 0f;
        public bool MovingUp => velocity.y > 0f;
        public bool MovingDown => velocity.y < 0f;

        public PlayerStatus(int maxHealth)
        {
            health = this.maxHealth = maxHealth;
        }

        public void CachePreviousStatus()
        {
        }
    }
}