using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// LineMover 需要分为自己动的(无父物体)和绕某个物体
/// </summary>
public class LineMover : BaseMover
{
    [SerializeField]
    private Vector3 endPos;
    public Vector2Int endXY;
    public float speed;
    
    public override float t
    {
        get
        {
            return _t;
        }
        set
        {
            if (value < 0) _t = 0;
            else if (value > 1) _t = 1;
        }
    }

    public override BaseMover parent
    {
        get => _parent;
        set => _parent = value;
    }

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    public override void Move(bool d,Vector3 delta)
    {
        foreach (var child in childMovers)
        {
            child.Move(Vector3.zero);
        }
        if (!IsMoving)return;
        direction = d;
        //边界判断
        var res = direction ? t + stepLength : t - stepLength;
        if (res < 0)
        {
            direction = true;
            t = 0;
        }
        else if(res>1)
        {
            direction = false;
            t = 1;
        }
        //GetDelta后 t已经被赋值了
        var myDelta = GetDelta(direction ? t + stepLength : t - stepLength);
        transform.Translate(delta+myDelta);
        foreach (var child in childMovers)
        {
            child.Move(delta+myDelta);
        }
    }
    public override void Move(Vector3 delta)
    {
        // if(!IsMoving)return;
        // var res = direction ? t + stepLength : t - stepLength;
        // if (res is < 0 or > 1) direction = !direction;
        // SetPos(direction?t+stepLength:t-stepLength);
        foreach (var child in childMovers)
        {
            child.Move(Vector3.zero);
        }
        if (!IsMoving)return;
        //边界判断
        var res = direction ? t + stepLength : t - stepLength;
        if (res < 0)
        {
            direction = true;
            t = 0;
        }
        else if(res>1)
        {
            direction = false;
            t = 1;
        }
        //GetDelta后 t已经被赋值了
        var myDelta = GetDelta(direction ? t + stepLength : t - stepLength);
        transform.Translate(delta+myDelta);
        foreach (var child in childMovers)
        {
            child.Move(delta+myDelta);
        }
    }
    public override Vector3 GetPos(float curT)
    {
        return Vector3.Lerp(startPos, endPos, curT);
    }
    public override void SetPos(float curT)
    {
        transform.position = parent.GetPosDelta() + GetPos(curT);
    }
}
