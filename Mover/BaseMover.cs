using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseMover : MonoBehaviour
{
    [HideInInspector]
    public bool IsMoving = true;
    protected float t;
    protected Vector3 startPos;
    protected Vector2Int startXY;
    
    public Transform parent;
    [HideInInspector]
    //t每秒要加的值
    public float stepLength = 1f;

    /// <summary>
    /// 自己的东西放在base.Start()前面
    /// </summary>
    protected virtual void Start()
    {
        
    }

    public abstract void Move(Vector3 parentPos, ref Vector3 outPos);

    /// <summary>
    /// 设置位置时通过该函数进行设置,不能直接修改transform.position
    /// </summary>
    /// <param name="pos"></param>
    public abstract void SetPosByPos(Vector3 pos);
    /// <summary>
    /// 如果不在范围内就返回最近的
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public abstract float GetTByPos(Vector3 pos);
    
    public abstract Vector3 GetPosByT(float _t);
    public abstract void SetPosByT(float _t);
}
