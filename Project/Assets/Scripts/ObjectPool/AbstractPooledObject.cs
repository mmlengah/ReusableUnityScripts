using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractPooledObject : MonoBehaviour
{
    protected ObjectPool<GameObject> pool;

    public void Initialize(AbstractObjectPool pool)
    {
        this.pool = pool.Pool;
    }
}
