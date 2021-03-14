using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HiryuTK.TopDownController
{
    public class Pool
    {
        Vector3 offscreen = new Vector3(-100, -100, -100);

        List<PoolObject> active = new List<PoolObject>();
        List<PoolObject> inactive = new List<PoolObject>();
        PoolObject prefab;
        Transform parent;

        public Pool(PoolObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
            if (prefab.GetComponent<PoolObject>() == null)
            {
                Debug.LogWarning("Object is not a valid PoolObject type");
            }
        }

        public PoolObject Spawn(Vector3 p, Quaternion r)
        {
            PoolObject go;
            if (inactive.Count > 0)
            {
                //If object pool is not empty, then take an object from the pool and make it active
                go = inactive[0];
                go.gameObject.SetActive(true);
                inactive.RemoveAt(0);
                go.transform.position = p;
                go.transform.rotation = r;
            }
            else
            {
                //If object pool is empty, then spawn a new object.
                go = GameObject.Instantiate(prefab, p, r, parent);
                go.GetComponent<PoolObject>().InitialSpawn(this);
            }
            go.GetComponent<PoolObject>().Activation(p, r);
            active.Add(go);
            return go;
        }

        public PoolObject Spawn(Vector3 p)
        {
            PoolObject go;
            if (inactive.Count > 0)
            {
                //If object pool is not empty, then take an object from the pool and make it active
                go = inactive[0];
                go.gameObject.SetActive(true);
                inactive.RemoveAt(0);
                go.transform.position = p;
            }
            else
            {
                //If object pool is empty, then spawn a new object.
                go = GameObject.Instantiate(prefab, p, Quaternion.identity, parent);
                go.GetComponent<PoolObject>().InitialSpawn(this);
            }
            go.GetComponent<PoolObject>().Activation(p);
            active.Add(go);
            return go;
        }

        public PoolObject Spawn()
        {
            PoolObject go;
            if (inactive.Count > 0)
            {
                //If object pool is not empty, then take an object from the pool and make it active
                go = inactive[0];
                go.gameObject.SetActive(true);
                inactive.RemoveAt(0);
            }
            else
            {
                //If object pool is empty, then spawn a new object.
                go = GameObject.Instantiate(prefab, offscreen, Quaternion.identity, parent);
                go.GetComponent<PoolObject>().InitialSpawn(this);
            }
            go.GetComponent<PoolObject>().Activation();
            active.Add(go);
            return go;
        }

        public void Despawn(PoolObject obj)
        {
            if (active.Contains(obj))
            {
                inactive.Add(obj);
                active.Remove(obj);
                obj.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Error, can't find object " + obj.name + "in pool. ");
            }
        }
    }
}