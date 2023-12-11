using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
public class CircleMover : BaseMover
{
    // private float radius = HexMap.innerRadius*2;
    public float radius ;
    /// <summary>
    /// 指的是x的绝对差与y绝对差的和  比如(1,1) 与 (2,2) 半径即为2 (两相邻的格子半径就是2了)
    /// </summary>
    // [Header("半径")]
    // public int HexRadius;
    [Header("周期")]
    public float period;
    
    public override float t
    {
        get => _t;
        set
        {
            if (value < 0) _t = value + 6;
            else if (value > 6) _t = value - 6;
            else _t = value;
        }
    }

    /// <summary>
    /// parent被设置时自动调整t的位置
    /// </summary>
    public override BaseMover parent
    {
        get => _parent;
        set
        {
            //被设置为父物体
            if (ReferenceEquals(value,GameObjectUtility.BaseMoverObj))
            {
                _parent = value;
            }
            //被设置为子物体
            else
            {
                t = value.t+3;
                _parent = value;
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        _t = 0f;
        stepLength = 0.1f;
        stepLength = stepLength*0.02f / period * 6;
        
    }
    
    public override void Move(bool d,Vector3 delta)
    {
        foreach (var child in childMovers)
        {
            child.Move(Vector3.zero);
        }
        if (!IsMoving)return;
        direction = d;
        var myDelta = GetDelta(direction ? t + stepLength : t - stepLength);
        t += direction?stepLength : -stepLength;
        transform.Translate(delta+myDelta);
        foreach (var child in childMovers)
        {
            child.Move(delta+myDelta);
        }
    }

    public override void Move(Vector3 delta)
    {
        foreach (var child in childMovers)
        {
            child.Move(Vector3.zero);
        }
        if (!IsMoving)return;
        var myDelta = GetDelta(direction ? t + stepLength : t - stepLength);
        t += direction?stepLength : -stepLength;
        transform.Translate(delta+myDelta);
        foreach (var child in childMovers)
        {
            child.Move(delta+myDelta);
        }
    }

    public override Vector3 GetPos(float curT)
    {
        return new Vector3(Mathf.Sin(t/3*Mathf.PI) * radius, Mathf.Cos(t/3*Mathf.PI) * radius);
    }
    public override void SetPos(float curT)
    {
        t = curT;
        transform.position = ReferenceEquals(parent, GameObjectUtility.BaseMoverObj)
            ? GetPos(_t)
            : parent.transform.position + GetPos(_t);
    }
    //
    // public override Vector3 GetDelta(float newT)
    // {
    //     t = newT;
    //     return GetPos(newT) - transform.position;
    // }
}
