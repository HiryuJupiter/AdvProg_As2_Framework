using System.Collections;
using UnityEngine;
using HiryuTK.UI.WorldPositionUIText;

namespace ClickAndDrag
{
    public class DragAndShoot : MonoBehaviour
    {
        [SerializeField] float forceAmmount = 500;
        [SerializeField] float minMagnitude = 1;
        [SerializeField] float maxMagnitude = 20;

        //Reference
        ScoreTextSpawner uiM;

        //Drag status
        Rigidbody dragTarget;
        float distToObject;
        Vector3 dragPositionOffset;
        Vector3 startingPosition;
        float pullDistance;

        bool DragEnabled = true;

        //Cache
        float maxMagnitudeSqr;
        float minMagnitudeSqr;

        public void EnableDrag(bool enable) => DragEnabled = enable;


        void Awake()
        {
            minMagnitudeSqr = minMagnitude * minMagnitude;
            maxMagnitudeSqr = maxMagnitude * maxMagnitude;
        }

        void Start()
        {
            uiM = ScoreTextSpawner.Instance;
        }

        private void Update()
        {
            if (DragEnabled)
            {
                ClickAndDrag();
                MoveTarget();
                ReleaseAndShoot();

            }
        }

        void ClickAndDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    distToObject = Vector3.Distance(hit.point, ray.origin);
                    dragTarget = hit.rigidbody;
                    dragPositionOffset = dragTarget.position - hit.point;
                    startingPosition = dragTarget.position;
                }
            }
        }

        void MoveTarget()
        {
            if (dragTarget)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 mousePos = ray.origin + ray.direction * distToObject;
                Vector3 targetMovePosition = mousePos + dragPositionOffset;

                //Clamp drag distance
                Vector3 direction = targetMovePosition - startingPosition;
                pullDistance = direction.magnitude;
                if (pullDistance > maxMagnitude)
                {
                    direction = direction.normalized * maxMagnitude;
                    pullDistance = maxMagnitude;

                }

                //uiM.SetPullDistance((int)pullDistance);
                dragTarget.position = startingPosition + direction;
            }
        }
        void ReleaseAndShoot()
        {
            if (dragTarget && Input.GetMouseButtonUp(0))
            {
                //Shoot target
                Vector3 direction = startingPosition - dragTarget.position;
                dragTarget.velocity = direction * forceAmmount;

                //Clear target
                dragTarget = null;
            }
        }
    }
}