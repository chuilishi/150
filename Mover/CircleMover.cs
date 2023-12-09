using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
public class CircleMover : BaseMover
{
    [Header("半径")]
    public float radius;
    [Header("周期")]
    public float period;
    private Vector2 center = Vector2.zero;
    [Button]
    public void AdjustPos()
    {
        if (parent != null)center = parent.transform.position;
        transform.position = new Vector3(Mathf.Sin(t*Mathf.PI) * radius + center.x, Mathf.Cos(t*Mathf.PI) * radius + center.y);
    }
    //inspector中的值被修改后执行
    // private void OnValidate()
    // {
    //     AdjustPos();
    // }
    protected override void Start()
    {
        stepLength = 2 / 50f / period;
        StartMoving();
        base.Start();
    }

    protected override void Move()
    {
        base.Move();
        transform.position = parent.transform.position + new Vector3(Mathf.Sin(t*Mathf.PI)*radius, Mathf.Cos(t*Mathf.PI)*radius);
        t += stepLength;
    }
}
