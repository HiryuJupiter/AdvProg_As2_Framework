using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

namespace HiryuTK.TopDownController
{
    public class HUDManager : MonoBehaviour
    {
        [Header("Group reference")]
        [SerializeField] GameObject HUD_Group;

        [Header("Image bars")]
        [SerializeField] Image healthBar;
        [SerializeField] Image manaBar;
        [SerializeField] Image staminaBar;

        [Header("Money")]
        [SerializeField] Text money;

        [Header("Color gradient")]
        [SerializeField] Gradient gradient;

        void Awake()
        {
        }

        #region HP MP AP
        public void SetHealthBar(float percentage)
        {
            healthBar.fillAmount = Mathf.Clamp01(percentage);
        }

        public void SetManaBar(float percentage) => manaBar.fillAmount = Mathf.Clamp01(percentage);

        public void SetStaminaBar(float percentage) => staminaBar.fillAmount = Mathf.Clamp01(percentage);

        #endregion


        public void SetMoney(int amount)
        {
            money.text = amount.ToString();
        }

        public void FlashDamageBorder()
        {
        }

        public void SetIsVisible(bool isVisible) => HUD_Group.SetActive(isVisible);
    }

    //     healthBar.color = gradient.Evaluate(percentage);
}