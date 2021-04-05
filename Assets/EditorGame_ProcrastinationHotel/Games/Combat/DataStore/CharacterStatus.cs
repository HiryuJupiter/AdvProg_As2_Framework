using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public class CharacterStatus
    {
        const int MaxHP = 100;

        public int Hp { get; private set; } = MaxHP;

        /// <summary>
        /// Modify the health value with an integer
        /// </summary>
        /// <param name="amount"></param>
        public void ModifyHealth(int amount)
        {
            Hp += amount;
            if (Hp < 0)     
                Hp = 0;
            if (Hp > 100)   
                Hp = 100;
        }

        public void Reset ()
        {
            Hp = MaxHP;
        }
    }
}