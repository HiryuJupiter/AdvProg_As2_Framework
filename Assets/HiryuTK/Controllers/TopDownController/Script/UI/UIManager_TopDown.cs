using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HiryuTK.TopDownController
{
    public class UIManager_TopDown : MonoBehaviour
    {
        public static UIManager_TopDown Instance;

        //public Image healthFG;
        public CanvasGroup deathScreen;
        public Text MoneyAmount;

        public void SetHealth (float percentage)
        {
            //healthFG.fillAmount = percentage;
        }

        public void SetMoney (int money)
        {
            MoneyAmount.text = money.ToString("00");
        }

        public void SetDeathScreenVisibility (bool isVisible)
        {
            //deathScreen.enabled = isVisible;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}