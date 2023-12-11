using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseMover : MonoBehaviour
{
    [HideInInspector]
    public bool IsMoving = true;
    public float MaxT = 1;
    protected float _t = 0f;
    public abstract float t { get; set; }
    protected Vector3 startPos;
    protected Vector2Int startXY;
    public BaseMover _parent;
    public abstract BaseMover parent { get; set; }
    [HideInInspector]
    //t每秒要加的值
    public float stepLength = 1f;

    /// <summary>
    /// 自己的东西放在base.Start()前面
    /// </summary>
    protected virtual void Start()
    {
        
    }
    public abstract void Move();
    public abstract Vector3 GetPos(float curT);
    public abstract void SetPos(float curT);

    public float GetNearestT()
    {
        return Mathf.RoundToInt(t);
    }
}
