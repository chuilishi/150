using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
[RequireComponent(typeof(MainMover))]
public class MoveBase : MonoBehaviour
{
    // public GameObject parent;
    /// <summary>
    /// 对于值为1的stepLength来说,每秒移动的距离为1
    /// </summary>
    [HideInInspector]
    //t每秒要加的值
    public float stepLength = 1f;
    protected bool isMoving = false;
    public virtual void Start()
    {
        
    }
}
