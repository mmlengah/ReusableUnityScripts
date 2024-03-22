using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractPoolSpawner : MonoBehaviour
{
    [SerializeField]
    private AbstractObjectPool objectPool;

    protected ObjectPool<GameObject> pool;

    void Awake()
    {
        pool = objectPool.Pool;
    }
}
