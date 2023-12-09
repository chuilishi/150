using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseMover : MonoBehaviour
{
    private bool _isMoving = false;
    protected float t;
    protected Vector3 originPos;
    public BaseMover parent;
    public delegate void FixedUpdateCallBackDelegate();
    [HideInInspector] public bool isRootMover = false;
    public event FixedUpdateCallBackDelegate FixedUpdateCallBack = () => {};
    [HideInInspector]
    //t每秒要加的值
    public float stepLength = 1f;
    
    /// <summary>
    /// 自己的东西放在base.Start()前面
    /// </summary>
    protected virtual void Start()
    {
        if (parent == null)
        {
            parent = GameObjectUtility.BaseMoverObj;
        }
        parent.FixedUpdateCallBack += () =>
        {
            Move();
            FixedUpdateCallBack.Invoke();
        };
    }
    protected virtual void Move()
    {
        if (!_isMoving) return;
    }
    
    // private void FixedUpdate()
    // {
    //     if (isRootMover)
    //     {
    //         Move();
    //         FixedUpdateCallBack.Invoke();
    //     }
    // }

    public void Reset()
    {
        t = 0;
        transform.position = originPos;
    }
    public void StartMoving()
    {
        _isMoving = true;
    }

    public void PauseMoving()
    {
        _isMoving = false;
    }
    public void StopMoving()
    {
        _isMoving = false;
        t = 1;
    }
    public void SetStartPos(Vector3 pos)
    {
        originPos = pos;
        transform.position = pos;
        Reset();
    }
}
