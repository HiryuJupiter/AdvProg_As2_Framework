using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class DummyMiner : MonoBehaviour
    {
        private const float litDuration = 1f;

        public LayerMask groundLayer;
        public LineRenderer lineRenderer;
        public Color miningColor = Color.yellow;

        float timer;

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.K))
            {
                Debug.Log("player pressed mining button");
                ShootMiningBeam();
            }

            timer -= Time.deltaTime;
            if (timer <= 0f)
                lineRenderer.enabled = false;
        }

        private void ShootMiningBeam()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, float.PositiveInfinity, groundLayer);

            timer = litDuration;
            lineRenderer.enabled = true;

            if (hit.collider != null) //maybe hit.collider != null 
            {
                //Debug.DrawLine(transform.position, hit.point, Color.red, 1f);
                //lineRenderer.startColor = miningColor;
                lineRenderer.startColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                lineRenderer.endColor = miningColor;
                lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point });
                lineRenderer.widthMultiplier = 0.1f;

                IPrism prism = hit.collider.GetComponent<IPrism>();
                prism.Charge(miningColor);
            }
            else
            {
                Debug.DrawLine(transform.position, mousePos, Color.blue, 1f);
                lineRenderer.SetPositions(new Vector3[] { transform.position, mousePos });
                lineRenderer.widthMultiplier = 0.05f;
                lineRenderer.startColor = Color.grey;
                lineRenderer.endColor = Color.grey;
            }

        }
    }
}