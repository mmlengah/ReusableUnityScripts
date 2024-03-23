using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCubePooledObject : AbstractPooledObject
{
    private Rigidbody rb;
    private float enableTime;
    private static bool collisionsDisabled = false; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        DisableLayerCollisionsOnce();
    }

    private void OnEnable()
    {
        SetRandomPositionAndRotation();
        enableTime = Time.time;
    }

    private static void DisableLayerCollisionsOnce()
    {
        if (!collisionsDisabled)
        {
            Physics.IgnoreLayerCollision(4, 4, true); 
            collisionsDisabled = true;
        }
    }

    private void SetRandomPositionAndRotation()
    {
        const float upForce = 11.2f;
        const float sideForce = 2.5f;
        const float rotationIntensity = 100f;

        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new(xForce, yForce, zForce);
        rb.velocity = force;

        Vector3 rotation = new(
            Random.Range(-rotationIntensity, rotationIntensity),
            Random.Range(-rotationIntensity, rotationIntensity),
            Random.Range(-rotationIntensity, rotationIntensity)
        );
        rb.angularVelocity = rotation * Mathf.Deg2Rad;
    }

    private void Update()
    {
        if (Time.time - enableTime >= 20f)
        {
            pool.Release(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - enableTime > 0.2f && collision.gameObject.CompareTag("Respawn"))
        {
            pool.Release(this.gameObject);
        }
    }
}
