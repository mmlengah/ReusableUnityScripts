using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCubeObjectPool : AbstractObjectPool
{
    protected override int DefaultCapacity => 5000;
    protected override int MaxSize => 7500;
    protected override bool CollectionCheck => true;

    private void FixedUpdate()
    {
        Debug.Log(ActiveObjectCount);
    }
}
