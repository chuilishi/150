using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
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
        base.Start();
        stepLength = 0.02f / period;
    }

    public override void Move(Vector3 parentPos,ref Vector3 outPos)
    {
        if (!IsMoving)
        {
            outPos = transform.position;
            return;
        }
        t += stepLength;
        outPos = transform.position = parentPos + GetPosByT(t);
    }

    public override void SetPosByPos(Vector3 pos)
    {
        t = GetTByPos(pos);
        transform.position = GetPosByT(t);
    }

    public override float GetTByPos(Vector3 myPos)
    {
        Vector3 minus = myPos - parent.transform.position;
        // return
        // Mathf.Atan2(minus.y, minus.x) < 0
        //     ? 2 * Mathf.PI + Mathf.Atan2(minus.y, minus.x)
        //     : Mathf.Atan2(minus.y, minus.x);
        return Mathf.Atan2(minus.y, minus.x)/Mathf.PI;
    }

    public override Vector3 GetPosByT(float _t)
    {
        return new Vector3(Mathf.Sin(_t*2*Mathf.PI)*radius, Mathf.Cos(_t*2*Mathf.PI)*radius);
    }

    public override void SetPosByT(float _t)
    {
        t = _t;
        transform.position = GetPosByT(_t);
    }
}
