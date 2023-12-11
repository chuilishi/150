using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
public abstract class BaseMover : MonoBehaviour
{
    [HideInInspector]
    public bool IsMoving = true;
    protected float _t = 0f;
    public abstract float t { get; set; }
    [SerializeField]
    protected Vector3 startPos;
    protected Vector2Int startXY;

    public BaseMover _parent;
    //记录物体上一帧的位置
    public Vector3 lastPos = Vector3.zero;
    //子物体的List
    public List<BaseMover> childMovers = new List<BaseMover>();
    /// <summary>
    /// 运动方向
    /// </summary>
    public bool direction = false;
    public abstract BaseMover parent { get; set; }
    [HideInInspector]
    //t每秒要加的值
    public float stepLength = 0.1f;
    /// <summary>
    /// 自己的东西放在base.Start()前面
    /// </summary>
    protected virtual void Start()
    {
        parent = GameObjectUtility.BaseMoverObj;
    }
    public abstract void Move(bool direction,Vector3 delta);
    public abstract void Move(Vector3 delta);
    public abstract Vector3 GetPos(float curT);
    public abstract void SetPos(float curT);

    public float GetNearestT()
    {
        return Mathf.RoundToInt(t);
    }
    [Button]
    public void AdjustPos(float paramT)
    {
        t = paramT;
        transform.position = GetPos(GetNearestT());
    }
    public Vector3 GetPosDelta()
    {
        return transform.position - lastPos;
    }

    public virtual void SetParent(BaseMover mover)
    {
        if (mover.parent.childMovers.Contains(this)) mover.parent.childMovers.Remove(this);
        parent = mover;
        mover.childMovers.Add(this);
    }

    public virtual Vector3 GetDelta(float newT)
    {
        t = newT;
        return GetPos(newT) - transform.position;
    }
}
