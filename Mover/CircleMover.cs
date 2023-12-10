using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(Rigidbody2D))]
public class CircleMover : BaseMover
{
    private float radius = HexMap.innerRadius*2;
    /// <summary>
    /// 指的是x的绝对差与y绝对差的和  比如(1,1) 与 (2,2) 半径即为2 (两相邻的格子半径就是2了)
    /// </summary>
    // [Header("半径")]
    // public int HexRadius;
    [Header("周期")]
    public float period;
    private float velocity;
    private Vector2 center = Vector2.zero;

    private Rigidbody2D _rb;
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
        base.Start();
        velocity = 2 * Mathf.PI / period;
        _rb = GetComponent<Rigidbody2D>();
        stepLength = 2 / 50f / period;
    }

    public override void Move(Vector3 parentPos,ref Vector3 outPos)
    {
        if (!IsMoving)
        {
            outPos = transform.position;
            return;
        }
        var direction = new Vector2(-(transform.position - parentPos).y, (transform.position - parentPos).x).normalized;
        _rb.velocity = direction * velocity;
        t += stepLength;
        outPos = transform.position = parentPos + GetPosByT(t);
    }

    public override void SetPos(Vector3 pos)
    {
        transform.position = GetPosByT(GetTByPos(pos));
    }

    public override float GetTByPos(Vector3 myPos)
    {
        Vector3 minus = myPos - parent.transform.position;
        return Mathf.Atan2(minus.y,minus.x)/Mathf.PI;
    }

    public override Vector3 GetPosByT(float _t)
    {
        return new Vector3(Mathf.Sin(_t*Mathf.PI)*radius, Mathf.Cos(_t*Mathf.PI)*radius);
    }

    public override void SetPosByT(float _t)
    {
        
    }
}
