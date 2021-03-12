using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HiryuTK.UI.WorldPositionUIText
{
    public class ScoreTextSpawner : MonoBehaviour
    {
        public static ScoreTextSpawner Instance;

        [Header("Scene object reference")]
        [SerializeField] Canvas canvas;

        [Header("Prefab")]
        [SerializeField] GameObject pf_scoreText;

        UIScoreTextObjectPool pool_ScoreText;

        void Awake()
        {
            Instance = this;
            pool_ScoreText = new UIScoreTextObjectPool(pf_scoreText, canvas);
        }

        public void SpawnScoreText(Vector3 targetWorldPosition, int score)
        {
            pool_ScoreText.Spawn(score.ToString(), targetWorldPosition);
        }
    }
}