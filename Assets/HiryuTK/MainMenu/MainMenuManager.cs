
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using HiryuTK.GameDataManagement;

namespace HiryuTK.MainMenu
{
    [RequireComponent(typeof(SceneLoader))]
    public class MainMenuManager : MonoBehaviour
    {
        const int SceneOne = 1;

        SceneLoader sceneLoader;
        GameDataManager gameDataContainer;
        SfxManager sfxManager;

        private void Start()
        {
            sceneLoader = GetComponent<SceneLoader>();
            sfxManager = SfxManager.instance;

            gameDataContainer = GameDataManager.Instance;
        }

        #region Public
        public void StartNewGame()
        {
            sceneLoader.LoadLevel(SceneOne);
        }

        public void ContinueGame()
        {
            gameDataContainer.LoadGameData();
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameDataContainer.Data.currentSceneIndex);
        }

        public void Clicked_Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
        #endregion
    }
}
   