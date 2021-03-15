using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace HiryuTK.TopDownController
{
    public class ShipState_Dead : ShipStateBase
    {
        public ShipState_Dead(PlayerTopDown3DController player, PlayerFeedbacks feedbacks) : base(player, feedbacks)
        {
            modules = new List<ModuleBase>()
            {
                
            };
        }

        public override void StateEntry()
        {
            base.StateEntry();
            feedback.SetModelVisibility(false);

            player.StartCoroutine(ReloadLevel());
        }

        private IEnumerator ReloadLevel ()
        {
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}