using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform spawnLocation;
    private static Transform pooledObjectsContainer;

    private int activeObjectCount = 0;
    public int ActiveObjectCount => activeObjectCount;

    public ObjectPool<GameObject> Pool { get; private set; }

    protected abstract int DefaultCapacity { get; }
    protected abstract int MaxSize { get; }
    protected abstract bool CollectionCheck { get; }

    protected virtual void Awake()
    {
        if (pooledObjectsContainer == null)
        {
            GameObject containerObject = new($"{prefab.name}Container");
            pooledObjectsContainer = containerObject.transform;
        }

        InitializePool(CollectionCheck, DefaultCapacity, MaxSize);
    }

    private void InitializePool(bool collectionCheck, int defaultCapacity, int maxSize)
    {
        Pool = new ObjectPool<GameObject>(
            createFunc: CreatePooledObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    protected virtual GameObject CreatePooledObject()
    {
        GameObject obj = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);
        obj.transform.SetParent(pooledObjectsContainer, false);

        if (obj.TryGetComponent(out AbstractPooledObject poolObjectComponent))
        {
            poolObjectComponent.Initialize(this);
        }
        else
        {
            Debug.LogWarning("The instantiated object does not have an AbstractPooledObject component.");
        }

        activeObjectCount++;
        return obj;
    }

    protected virtual void OnGetObject(GameObject obj)
    {
        obj.transform.SetPositionAndRotation(spawnLocation.position, spawnLocation.rotation);
        obj.SetActive(true);

        activeObjectCount++;
    }

    protected virtual void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);

        activeObjectCount--;
    }

    protected virtual void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
        activeObjectCount--;
    }
}
