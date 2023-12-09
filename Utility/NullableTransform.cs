using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullableTransform : MonoBehaviour
{
    public Vector3 position;
    private void Awake()
    {
        position = transform.position;
    }
}
