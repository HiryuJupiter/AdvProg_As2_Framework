using System.Collections;
using UnityEngine;

namespace HiryuTK.UI.WorldPositionUIText
{
    public interface IUITextPoolable
    {
        void SetUp(UIScoreTextObjectPool pool, Canvas canvas);
        void Activation(string text, Vector3 targetPosition);
    }
}