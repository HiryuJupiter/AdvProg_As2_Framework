using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HiryuTK.KeyBind
{
    /// <summary>
    /// The entry point from Options menu into the key-remapping system.
    /// </summary>
    [RequireComponent(typeof(KeyRemapperUIReference))]
    [DefaultExecutionOrder(-100)]
    public class KeyRemappingManager : MonoBehaviour
    {
        public static KeyRemappingManager Instance;

        #region Variables
        [Header("Colors")]
        public Color32 ButtonColor_Default;
        public Color32 ButtonColor_Modifying;

        public bool IsListeningForKey { get; private set; }

        //Class reference
        private KeyRemapperUIReference ui;
        private SfxManager sfxManager;
        //KeybindReset resetter;

        //Status
        private GameObject currentButton;
        private Image activeButtonImage;
        private Text activeButtonText;
        private string previousStringOnButton;

        //Cache
        private Color32 textColor_Default;

        //This is for reverting the changes
        private Dictionary<string, KeyCode> bufferKeybind = new Dictionary<string, KeyCode>();
        #endregion

        #region Mono
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        #region Public
        public void Initialize()
        {
            //Reference
            ui = GetComponent<KeyRemapperUIReference>();
            sfxManager = SfxManager.instance;

            //Load keyscheme and update display
            UpdateKeySchemeFromPlayerPrefs();
            ui.RefreshUIButtonTextDisplay();
        }

        public void ConfirmSaveAllKeybinds()
        {
            foreach (var b in bufferKeybind)
            {
                SaveKeycodeToPlayerPrefs(b.Key, b.Value);
                //Lookup.GetBtnText(b.Key).text = Lookup.GetKeycode(b.Key).ToString();
            }

            UpdateKeySchemeFromPlayerPrefs();
        }

        public void UpdateKeySchemeFromPlayerPrefs()
        {
            KeyScheme.Up = (KeyCode)PlayerPrefs.GetInt(Keystring.Up, (int)KeyCode.W);
            KeyScheme.Down = (KeyCode)PlayerPrefs.GetInt(Keystring.Down, (int)KeyCode.S);
            KeyScheme.Left = (KeyCode)PlayerPrefs.GetInt(Keystring.Left, (int)KeyCode.A);
            KeyScheme.Right = (KeyCode)PlayerPrefs.GetInt(Keystring.Right, (int)KeyCode.D);
            KeyScheme.Jump = (KeyCode)PlayerPrefs.GetInt(Keystring.Jump, (int)KeyCode.Space);
        }
        #endregion

        #region Public - UI Click Event
        public void EnterKeyBind(GameObject button)
        {
            if (!IsListeningForKey)
            {
                sfxManager.SpawnUI_4_shake();
                IsListeningForKey = true;

                //Cache references
                currentButton = button;
                activeButtonText = button.GetComponentInChildren<Text>();
                activeButtonImage = button.GetComponent<Image>();
                previousStringOnButton = activeButtonText.text;

                //Highlight active button
                activeButtonImage.color = ButtonColor_Modifying;
                activeButtonText.text = "???";
                textColor_Default = activeButtonText.color;
                activeButtonText.color = Color.white;

                //Begin listening for an input
                StartCoroutine(ListenForKeyInput());
            }
        }

        public void ResetKeys()
        {
            SetBufferKeybind(Keystring.Up, KeyCode.W);
            SetBufferKeybind(Keystring.Down, KeyCode.S);
            SetBufferKeybind(Keystring.Left, KeyCode.A);
            SetBufferKeybind(Keystring.Right, KeyCode.D);
            SetBufferKeybind(Keystring.Jump, KeyCode.Space);
        }
        #endregion

        #region Keybind
        private IEnumerator ListenForKeyInput()
        {
            while (IsListeningForKey)
            {
                if (Input.GetKeyDown(KeyCode.Escape)) //Exit keybind
                {
                    ExitKeyBind();
                    yield break;
                }

                if (Input.anyKeyDown)
                {
                    foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
                    {
                        //If player pressed down a valid key.
                        //We use this instead of OnGUI's Event.Current in order to listen for Joypad.Buttons
                        if ((int)keycode < 323 && Input.GetKeyDown(keycode))
                        {
                            //Prevents binding the Quit-button causes to quit the options menu on the same frame.
                            //StartCoroutine(menuM.PreventScreenChangeForOneFrame());

                            //Update UI Text
                            activeButtonText.text = keycode.ToString();

                            //Save (int)keycode to playerPref
                            string keystring = ui.GetBtnKey(currentButton);
                            SetBufferKeybind(keystring, keycode);
                            sfxManager.SpawnUI_4_shake();

                            //KeyScheme.SaveKeycodeToPlayerPrefs(keystring, keycode); //If you want to save 1 key at a time

                            //Debug.Log("Remapped " + currentText.name + " button: " + keycode);
                            ExitKeyBind();
                            yield break;
                        }
                    }
                }
                yield return null;
            }
        }

        private void SetBufferKeybind(string keystring, KeyCode keyCode)
        {
            bufferKeybind[keystring] = keyCode;

            ui.GetBtnText(keystring).text = keyCode.ToString();
            //Lookup.GetBtnText(keystring).text = Lookup.GetKeycode(keystring).ToString();
        }

        private void ExitKeyBind()
        {
            activeButtonImage.color = ButtonColor_Default;
            activeButtonText.color = textColor_Default;
            activeButtonText.text = previousStringOnButton;
            IsListeningForKey = false;
        }
        #endregion

        #region KeyScheme
        private void SaveKeycodeToPlayerPrefs(string stringkey, KeyCode keyCode)
        {
            PlayerPrefs.SetInt(stringkey, (int)keyCode);
        }

        private void ResetAllKeyScheme()
        {
            PlayerPrefs.SetInt(Keystring.Up,    (int)KeyCode.W);
            PlayerPrefs.SetInt(Keystring.Down,  (int)KeyCode.S);
            PlayerPrefs.SetInt(Keystring.Left,  (int)KeyCode.A);
            PlayerPrefs.SetInt(Keystring.Right, (int)KeyCode.D);
            PlayerPrefs.SetInt(Keystring.Jump,  (int)KeyCode.Space);

            UpdateKeySchemeFromPlayerPrefs();
        }
        #endregion
    }
}