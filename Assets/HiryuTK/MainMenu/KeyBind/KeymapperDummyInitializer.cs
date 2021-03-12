using UnityEngine;
using System.Collections;

namespace HiryuTK.KeyBind
{
    public class KeymapperDummyInitializer : MonoBehaviour
    {
        private void Start()
        {
            KeyRemappingManager.Instance.UpdateKeySchemeFromPlayerPrefs();
        }
    }
}