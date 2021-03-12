using System.Collections;
using UnityEngine;

namespace HiryuTK.ObjectPool
{
    /// <summary>
    /// ObjectPoolManager can reference this class instead of MonoBehiavour
    /// for referencing prefabs (note IPoolable cannot be referenced in
    /// inspector). Use this as the base class for enemy and bullet base
    /// classes.
    /// </summary>
    public abstract class PoolObject : MonoBehaviour
    {
        protected Pool pool;

        public void InitialSpawn(Pool pool)
        {
            this.pool = pool;
        }
        public virtual void Activation() { }
        public virtual void Activation(Vector3 p) 
        {
            transform.position = p;
            Activation();
        }

        public virtual void Activation(Vector3 p, Quaternion r) 
        {
            transform.position = p;
            transform.rotation = r;
            Activation();
        }

        protected void Despawn()
        {
            pool.Despawn(this);
        }
    }
}