using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseMover : MonoBehaviour
{
    [HideInInspector] public bool IsMoving = true;
    protected float _t = 0f;
    public abstract float t { get; set; }
    [SerializeField] protected Vector3 startPos;

    protected Vector2Int startXY;

    //父物体
    public BaseMover m_parent;

    //记录物体上一帧的位置
    public Vector3 lastPos = Vector3.zero;

    //子物体的List
    public List<BaseMover> childMovers = new List<BaseMover>();

    //本轮已经移动过了,不需要再移动了(一次FixedUpdate只能Move一次)
    public bool hasMoved = false;

    //运动方向
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
    }

    #region 两个移动脚本

    public virtual void Move(Vector3 ?parentPos = null ,bool d = false)
    {
        if(hasMoved)return;
        var nowPos = transform.position;
        if (!IsMoving)
        {
            hasMoved = true;
            foreach (var child in childMovers)
            {
                child.Move(nowPos);
            }
            return;
        }
        //没有parentPos就等于这个↓
        parentPos ??= parent.transform.position;
        if (d) direction = !direction;
        //GetDelta后 t已经被赋值了
        var myDelta = SetTAndGetDelta(direction ? t + stepLength : t - stepLength);
        transform.Translate(parentPos.Value + myDelta);
        nowPos = transform.position;
        hasMoved = true;
        foreach (var child in childMovers)
        {
            child.Move(nowPos);
        }
    }
    #endregion

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
        mover.childMovers.Remove(this);
        parent = mover;
        mover.childMovers.Add(this);
    }

    /// <summary>
    /// 物体位置是以自己的 变化量 + 父物体的pos 来决定的
    /// </summary>
    /// <param name="newT"></param>
    /// <returns></returns>
    public virtual Vector3 SetTAndGetDelta(float newT)
    {
        t = newT;
        return GetPos(newT) - transform.position;
    }
    public virtual float GetTByPos(Vector3 myPos)
    {
        Vector3 minus = myPos - parent.transform.position;
        float angle = Vector3.Angle(Vector3.right, minus);
        //如果y分量为负,那么角度就是360-angle
        if (minus.y < 0) angle = 360 - angle;
        return Mathf.Deg2Rad * angle / 6;
    }
}