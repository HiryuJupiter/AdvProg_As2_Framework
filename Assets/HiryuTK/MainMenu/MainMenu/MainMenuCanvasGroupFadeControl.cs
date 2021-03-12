using UnityEngine;
using System.Collections;
using System;

namespace HiryuTK.MainMenu
{
    public class MainMenuCanvasGroupFadeControl : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)]
        private float TransitionDuration = 0.2f;
        [SerializeField]
        private bool DisplaySplashOnStart = true;

        [SerializeField] private CanvasGroup Canvas_SplashScreen;
        [SerializeField] private CanvasGroup Canvas_MainMenu;
        [SerializeField] private CanvasGroup Canvas_OptionsMenu;
        [SerializeField] private CanvasGroup Canvas_AboutMenu;
        [SerializeField] private CanvasGroup Canvas_LoadingScreen;

        private SfxManager sfxManager;

        #region MonoBehaviour
        private void Awake()
        {
            CanvasGroupHelper.InstantHide(Canvas_MainMenu);
            CanvasGroupHelper.InstantHide(Canvas_OptionsMenu);
            CanvasGroupHelper.InstantHide(Canvas_AboutMenu);
            CanvasGroupHelper.InstantHide(Canvas_LoadingScreen);
            if (DisplaySplashOnStart)
            {
                OpenSplashScreen();
            }
            else
            {
                FadeInMainMenu();
            }
        }

        private void Start()
        {
            sfxManager = SfxManager.instance;
        }
        #endregion

        #region Public - transitions
        public void SplashToMain()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_SplashScreen, Canvas_MainMenu, TransitionDuration));
        }

        public void MainToOptions()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_MainMenu, Canvas_OptionsMenu, TransitionDuration));
        }

        public void OptionsToMain()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_OptionsMenu, Canvas_MainMenu, TransitionDuration));
        }

        public void MainToAbout()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_MainMenu, Canvas_AboutMenu, TransitionDuration));
        }

        public void AboutToMain()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_AboutMenu, Canvas_MainMenu, TransitionDuration));
        }

        public void MainToLoading()
        {
            PlaySfx();
            StartCoroutine(CanvasGroupHelper.CrossfadeCoroutine(Canvas_MainMenu, Canvas_LoadingScreen, TransitionDuration));
        }
        #endregion

        private IEnumerator OpenSplashScreen()
        {
            yield return StartCoroutine(
                CanvasGroupHelper.FadeInCoroutine(Canvas_SplashScreen, TransitionDuration));

            bool inSplash = true;
            while (inSplash)
            {
                if (Input.anyKeyDown)
                    inSplash = false;
                yield return null;
            }
            SplashToMain();
        }

        private void PlaySfx()
        {
            sfxManager.SpawnUI_1_Click();
        }

        private void FadeInMainMenu()
        {
            StartCoroutine(CanvasGroupHelper.FadeInCoroutine(Canvas_SplashScreen, TransitionDuration));
        }
    }
}