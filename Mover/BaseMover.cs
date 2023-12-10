using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseMover : MonoBehaviour
{
    protected bool IsMoving = false;
    protected float t;
    protected Vector3 startPos;
    protected Vector2Int startXY;
    
    public BaseMover parent;
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

    public abstract Vector2Int[] GetAccessiblePos(int x, int y);
}
