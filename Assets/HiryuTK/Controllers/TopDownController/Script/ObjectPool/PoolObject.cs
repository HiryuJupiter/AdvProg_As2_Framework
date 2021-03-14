using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    /// <summary>
    /// We use this instead an interface (e.g. IPoolable) because the inspector
    /// cannot reference interface. Thus, relying on this is more reliable 
    /// when assigning prefabs.
    /// The downside of this is that the prefab must inherit this as the 
    /// base class instead of MonoBehaviour. This can be an issue where 
    /// a prefab cannot do this. 
    /// </summary>
    public abstract class PoolObject : MonoBehaviour
    {
        protected Pool pool;

        public virtual void InitialSpawn(Pool pool)
        {
            this.pool = pool;
        }
        public virtual void Activation() { }
        public virtual void Activation(Vector2 p) 
        {
            transform.position = p;
            Activation();
        }

        public virtual void Activation(Vector2 p, Quaternion r) 
        {
            transform.position = p;
            transform.rotation = r;
            Activation();
        }

        protected virtual void Despawn()
        {
            pool.Despawn(this);
        }
    }
}