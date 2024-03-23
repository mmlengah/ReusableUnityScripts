using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ExampleCubePoolSpawner : AbstractPoolSpawner
{
    [SerializeField, Range(1, 30)]
    private int cubesPerFrame = 10;

    private void FixedUpdate()
    {
        for (int i = 0; i < cubesPerFrame; i++)
        {
            pool.Get();
        }
    }
}
