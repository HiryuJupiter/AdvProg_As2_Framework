using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "BGM", menuName = "BGM/BGMContainer")]
public class BGMContainer : ScriptableObject
{
    public AudioClip Song;
    public int[] LevelRange;

    public bool ContainsLevel(int level)
    {
        foreach (var item in LevelRange)
        {
            if (item == level)
                return true;
        }
        return false;
    }
}