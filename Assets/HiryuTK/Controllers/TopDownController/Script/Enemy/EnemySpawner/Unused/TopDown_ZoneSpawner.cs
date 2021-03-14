using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TafeDiplomaFramework.AI
{
    public class TopDown_ZoneSpawner : MonoBehaviour
    {
        public enum SpawnMode { XYZ, XY, XZ }

        [SerializeField] private SpawnMode spawnMode = SpawnMode.XYZ;
        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField, Range(0.01f, 10f)] private float spawnCDMin = 0f;
        [SerializeField, Range(0.02f, 5f)] private float spawnCDMax = 5f;
        [SerializeField] private GameObject prefab = null;

        //Status
        float spawnTimer = 0;

        //Cache
        float extentX;
        float extentY;
        float extentZ;

        #region Mono
        private void Awake()
        {
            extentX = size.x * transform.localScale.x * .5f;
            extentY = size.y * transform.localScale.y * .5f;
            extentZ = size.z * transform.localScale.z * .5f;
        }

        private void Start()
        {
            Spawn();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            spawnCDMin = Mathf.Clamp(spawnCDMin, 0f, spawnCDMax - 0.01f);
            spawnCDMax = Mathf.Clamp(spawnCDMax, spawnCDMin + 0.01f, float.MaxValue);
        }

        private void OnDrawGizmosSelected()
        {
            //Or use LocalToWorldMatrix when not nesting.
            Gizmos.matrix = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(Vector3.zero, new Vector3(
                size.x,
                spawnMode == SpawnMode.XZ ? 0f : size.y,
                spawnMode == SpawnMode.XY ? 0f : size.z));
        }
#endif
        #endregion

        private void Spawn()
        {
            Instantiate(prefab, GetSpawnPosition(), transform.rotation);
            StartCoroutine(StartCooldown());
        }

        private IEnumerator StartCooldown()
        {
            spawnTimer = Random.Range(spawnCDMin, spawnCDMax);
            while (spawnTimer > 0f)
            {
                spawnTimer -= Time.deltaTime;
                yield return null;
            }
            Spawn();
        }

        private Vector3 GetSpawnPosition()
        {
            float x = Random.Range(-extentX, extentX);
            float y = spawnMode == SpawnMode.XZ ? 0f : Random.Range(-extentY, extentY);
            float z = spawnMode == SpawnMode.XY ? 0f : Random.Range(-extentZ, extentZ);
            return transform.TransformPoint(new Vector3(x, y, z)); //Make it relative
        }
    }
}

/*
 private void OnDrawGizmosSelected() //Great for drawing range of towers
        {
            //Store default matrix
            Matrix4x4 baseMatrix = Gizmos.matrix; 

            //Matrix is like a 2D array, it's what Unity uses to draw.
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;

            //Draw a green, partially transparent cube
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(Vector3.zero, size);

            //Reset the gizmos matrix back to default
            Gizmos.matrix = baseMatrix;
        }
 */