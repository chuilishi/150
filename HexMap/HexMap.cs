using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 右x 左上y 左下z 
/// </summary>
public static class HexDirection
{
    public static Vector3Int Up = new Vector3Int(0,1,-1);
    public static Vector3Int Down = new Vector3Int(0,-1,1);
    public static Vector3Int UpperLeft = new Vector3Int(-1,1,0);
    public static Vector3Int UpperRight = new Vector3Int(1,-1,0);
    public static Vector3Int LowerLeft = new Vector3Int(-1,0,1);
    public static Vector3Int LowerRight = new Vector3Int(1,-1,0);
}

public class HexMap : MonoBehaviour
{
    public GameObject Circle;

    //定义一个Vector3Int 数组
    public Vector3Int[] hexCoordinates = new []
    {
        new Vector3Int()
    };
    //Edge Length of every Grid
    public float size;
    private static float innerRadius = 0.866025f/2;
    private static float outerRadius = 1f/2;
    
    //根据立体坐标获取Unity坐标
    //x,y,z 分别对应q,s(左上),r
    public Vector3 GetUnityPos(Vector3 hexCoordinate)
    {
        float unityY = (hexCoordinate.y - hexCoordinate.z) * Mathf.Sqrt(3) / 2 * outerRadius;
        float unityX = hexCoordinate.x * 1.5f * outerRadius;
        return new Vector3(unityX, unityY, 0);
    }
    /// <summary>
    /// 根据Unity坐标获取
    /// </summary>
    /// <returns></returns>
    public Vector3 GetHexPos(Vector3 unityPos)
    {
        var q = unityPos.x * 2 / 3 / outerRadius;
        var s = (-1f / 3 * unityPos.x + Mathf.Sqrt(3) / 3 * unityPos.y)/outerRadius;
        var r = -q - s;
        return new Vector3(q, s, r);
    }
    public Vector3 GetNearestUnityPos(float3 pos)
    {
        var hexpos = GetHexPos(pos);
        var a = Mathf.RoundToInt(hexpos.x);
        var b = Mathf.RoundToInt(hexpos.y);
        var c = Mathf.RoundToInt(hexpos.z);
        return GetUnityPos(new Vector3(a, b, c));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float3 pos = new float3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
            Instantiate(Circle, GetNearestUnityPos(pos),quaternion.identity);
        }
    }
}
