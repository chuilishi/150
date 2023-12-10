using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
public class CircleMover : BaseMover
{
    private float radius;
    /// <summary>
    /// 指的是x的绝对差与y绝对差的和  比如(1,1) 与 (2,2) 半径即为2 (两相邻的格子半径就是2了)
    /// </summary>
    [Header("半径")]
    public int HexRadius;
    [Header("周期")]
    public int period;
    private Vector2 center = Vector2.zero;
    [Button]
    public void AdjustPos()
    {
        if (parent != null)center = parent.transform.position;
        transform.position = new Vector3(Mathf.Sin(t*Mathf.PI) * radius + center.x, Mathf.Cos(t*Mathf.PI) * radius + center.y);
    }
    //inspector中的值被修改后执行
    // private void OnValidate()
    // {
    //     AdjustPos();
    // }
    protected override void Start()
    {
        base.Start();
        stepLength = 2 / 50f / period;
    }

    public override void Move(Vector3 parentPos,ref Vector3 outPos)
    {
        if (!IsMoving)return;
        outPos = transform.position = parent.transform.position + new Vector3(Mathf.Sin(t*Mathf.PI)*radius, Mathf.Cos(t*Mathf.PI)*radius);
        t += stepLength;
    }

    public override Vector2Int[] GetAccessiblePos(int x, int y)
    {
        // List<Vector2Int> res = new List<Vector2Int>();
        // res.Add(new Vector2Int(x,y+HexRadius));
        // res.Add(new Vector2Int(x,y-HexRadius));
        // res.Add(new Vector2Int(x-HexRadius,y));
        // res.Add(new Vector2Int(x+HexRadius,y));
        // for (int i = 1; i <= HexRadius; i++)
        // {
        //     res.Add(new Vector2Int(x-i,y-(HexRadius-i)));
        //     res.Add(new Vector2Int(x+i,y-(HexRadius-i)));
        //     res.Add(new Vector2Int(x-i,y+(HexRadius-i)));
        //     res.Add(new Vector2Int(x+i,y+(HexRadius-i)));
        // }
        return new Vector2Int[0];
    }
}
