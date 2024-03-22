using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCubeObjectPool : AbstractObjectPool
{
    protected override int DefaultCapacity => 1000;
    protected override int MaxSize => 2000;
    protected override bool CollectionCheck => true;
}
