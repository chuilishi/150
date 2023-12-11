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
            if (value < 0)
            {
                direction = true;
                t = 0;
            }
            else if(value>1)
            {
                direction = false;
                t = 1;
            }
        }
    }

    public override BaseMover parent
    {
        get => m_parent;
        set => m_parent = value;
    }

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
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
