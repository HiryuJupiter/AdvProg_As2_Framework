using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.CameraControl
{
    public enum HorizontalModes { panLeft, panRight, center }
    public class CameraFollow_horizontal : MonoBehaviour
    {
        #region Fields
        public Transform player;
        public HorizontalModes mode;

        //Cache
        float camStartingX;
        Vector3 playerPos;
        Vector3 targetPos;
        #endregion

        #region MonoBehaviour    
        private void Awake()
        {
            camStartingX = transform.position.x;
        }

        void Update()
        {
            playerPos = player.position;
            targetPos = transform.position;

            switch (mode)
            {
                case HorizontalModes.panLeft:
                    targetPos.x = (playerPos.x < camStartingX) ? playerPos.x : camStartingX;
                    break;
                case HorizontalModes.panRight:
                    targetPos.x = (playerPos.x > camStartingX) ? playerPos.x : camStartingX;
                    break;
                case HorizontalModes.center:
                default:
                    targetPos.x = playerPos.x;
                    break;
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, 2f * Time.deltaTime);
        }
        #endregion
    }
}