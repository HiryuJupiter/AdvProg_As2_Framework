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
        public Vector2 velocity;
        public float rotation;

        //Hurt
        public float hurtDuration;

        //Hurt state
        [HideInInspector] public Vector2 hurtDriftDirection;

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