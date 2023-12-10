using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LineMover : BaseMover
{
    private Vector3 endPos;
    public Vector2Int endXY;
    public float speed;
    
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }
    public override void Move(Vector3 parentPos, ref Vector3 outPos)
    {
        if (!IsMoving)
        {
            return;
        }
        t += stepLength/100*speed;
        outPos = transform.position = (Vector2)parentPos + Vector2.Lerp(startPos, endPos, t);
        if(t>=1)
        {
            t = 1;
            IsMoving = false;
        }
    }

    public override void SetPos(Vector3 pos)
    {
        Debug.LogError("暂时还没实现");
        return;
    }

    public override float GetTByPos(Vector3 pos)
    {
        
    }

    public override Vector3 GetPosByT(float _t)
    {
        
    }

    public override void SetPosByT(float _t)
    {
        t = _t;
        transform.position = GetPosByT(_t);
    }
}
