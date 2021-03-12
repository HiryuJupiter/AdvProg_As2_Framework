using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HiryuTK.TopDownController
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public Image healthFG;

        public CanvasGroup deathScreen;

        public void SetHealth (float percentage)
        {
            healthFG.fillAmount = percentage;
        }

        public void SetDeathScreenVisibility (bool isVisible)
        {
            deathScreen.enabled = isVisible;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}