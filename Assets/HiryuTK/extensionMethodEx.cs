using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HiryuTK
{
    //methods that you can write that aren't built in
    //They can only be accesse by script that have access to its namespace
    public class extensionMethodEx : MonoBehaviour
    {
        void Start()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Deselect;
            entry.callback.AddListener(data => OnDeselected((PointerEventData)data));
            trigger.triggers.Add(entry);
        }



        void Update()
        {
        
        }

        void OnDeselected(PointerEventData data)
        {

        }
    }

    

    public static class MyClass
    {
        //void SomSort (this List<T> list) where T:IComparable
    }


}
