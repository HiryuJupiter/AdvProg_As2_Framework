using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace HiryuTK.KeyBind
{
    public class KeyRemapperUIReference : MonoBehaviour
    {
        [SerializeField] Button Up;
        [SerializeField] Button Down;
        [SerializeField] Button Left;
        [SerializeField] Button Right;
        [SerializeField] Button Jump;

        private class UIReferencer
        {
            public string keystring;
            public GameObject button;
            public Text uiText;

            public UIReferencer(string key, GameObject button, Text uiText)
            {
                this.keystring = key;
                this.button = button;
                this.uiText = uiText;
            }
        }

        private List<UIReferencer> lookups;

        private void Awake()
        {
            //Look up dictionary initializations
            lookups = new List<UIReferencer>()
            {
                new UIReferencer(Keystring.Up,     Up.gameObject,      Up.GetComponentInChildren<Text>()),
                new UIReferencer(Keystring.Down ,  Down.gameObject,    Down.GetComponentInChildren<Text>()),
                new UIReferencer(Keystring.Left,   Left.gameObject,    Left.GetComponentInChildren<Text>()),
                new UIReferencer(Keystring.Right , Right.gameObject,   Right.GetComponentInChildren<Text>()),
                new UIReferencer(Keystring.Jump,   Jump.gameObject,    Jump.GetComponentInChildren<Text>())
            };
        }

        #region Public
        public void RefreshUIButtonTextDisplay()
        {
            GetBtnText(Keystring.Up).text = KeyScheme.Up.ToString();
            GetBtnText(Keystring.Down).text = KeyScheme.Down.ToString();
            GetBtnText(Keystring.Left).text = KeyScheme.Left.ToString();
            GetBtnText(Keystring.Right).text = KeyScheme.Right.ToString();
            GetBtnText(Keystring.Jump).text = KeyScheme.Jump.ToString();
        }

        public Text GetBtnText(string key)
        {
            return lookups.FirstOrDefault(x => x.keystring == key).uiText;
        }

        public string GetBtnKey(GameObject button)
        {
            return lookups.FirstOrDefault(x => x.button == button).keystring;
        }
        #endregion
    }
}