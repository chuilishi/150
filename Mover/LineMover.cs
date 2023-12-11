using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LineMover : BaseMover
{
    private Vector3 endPos;
    public Vector2Int endXY;
    public float speed;
    public override float t { get; set; }

    public override BaseMover parent
    {
        get => _parent;
        set
        {
            _parent = value;
        }
    }

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    public override void Move()
    {
        
    }
    

    public override Vector3 GetPos(float curT)
    {
        throw new NotImplementedException();
    }

    public override void SetPos(float curT)
    {
        throw new NotImplementedException();
    }
}
