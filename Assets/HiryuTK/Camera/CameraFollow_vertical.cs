using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.CameraControl
{
    public enum VerticalModes { panDown, panUp, center }
    public class CameraFollow_vertical : MonoBehaviour
    {
        #region Fields
        public Transform player;
        public VerticalModes mode;
        public float lerpSpeed = 2f;

        //Cache
        float camStartingY;
        Vector3 playerPos;
        Vector3 targetPos;
        #endregion

        #region MonoBehaviour    
        private void Awake()
        {
            camStartingY = transform.position.y;
        }

        void Update()
        {
            playerPos = player.position;
            targetPos = transform.position;

            switch (mode)
            {
                case VerticalModes.panDown:
                    targetPos.y = (playerPos.y < camStartingY) ?
                        playerPos.y : camStartingY;
                    break;
                case VerticalModes.panUp:
                    targetPos.y = (playerPos.y > camStartingY) ?
                        playerPos.y : camStartingY;
                    break;
                case VerticalModes.center:
                default:
                    targetPos.y = playerPos.y;
                    break;
            }

            transform.position = Vector3.Lerp(transform.position, targetPos,
                lerpSpeed * Time.deltaTime);
        }
        #endregion
    }
}