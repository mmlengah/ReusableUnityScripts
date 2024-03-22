using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ExampleCubePoolSpawner : AbstractPoolSpawner
{
    private float startTime;
    private readonly float duration = 60f; 
    private readonly float maxWaitTime = 0.2f;
    private readonly float minWaitTime = 0f;

    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(SpawnCubeRoutine());
    }

    private IEnumerator SpawnCubeRoutine()
    {
        while (true)
        {
            float elapsedTime = Time.time - startTime; 
            float currentWaitTime = Mathf.Lerp(maxWaitTime, minWaitTime, elapsedTime / duration);
            currentWaitTime = Mathf.Max(currentWaitTime, minWaitTime); 

            yield return new WaitForSeconds(currentWaitTime);

            GameObject cube = pool.Get();
            if (cube != null)
            {
                cube.transform.position = Vector3.zero;
                cube.SetActive(true);
            }
        }
    }
}
