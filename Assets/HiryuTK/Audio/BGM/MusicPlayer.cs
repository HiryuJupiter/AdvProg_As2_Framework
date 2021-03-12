using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    public BGMContainer[] Songs;

    [SerializeField] AudioSource audioSource;
    private bool fadingOut;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            OnLevelWasLoaded(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AboutToChangeLevel(int nextLevelIndex)
    {
        if (!DoesTheNextLevelUsesTheSameMusic(nextLevelIndex))
        {
            FadeOut();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        fadingOut = false;
        audioSource.volume = 1f;
        if (TryGetSong(level, out AudioClip clip))
        {
            if (clip != audioSource.clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        else
        {
            Debug.Log("Playlist does not contain a song for this level. Index = " + level);
            //audioSource.clip = clip;
            //audioSource.Play();
        }
    }

    IEnumerator FadeOut()
    {
        float v = audioSource.volume;
        fadingOut = true;
        while (v > 0f && fadingOut)
        {
            v -= Time.deltaTime;
            yield return null;
        }
        fadingOut = false;
    }

    bool DoesTheNextLevelUsesTheSameMusic(int nextLevel)
    {
        foreach (var item in Songs)
        {
            if (item.ContainsLevel(nextLevel))
            {
                return true;
            }
        }
        return false;
    }

    bool TryGetSong(int level, out AudioClip clip)
    {
        clip = null;
        foreach (var item in Songs)
        {
            if (item.ContainsLevel(level))
            {
                clip = item.Song;
                return true;
            }
        }
        return false;
    }
}
