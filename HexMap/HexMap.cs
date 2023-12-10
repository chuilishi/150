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

    public Vector3Int hexCoordinate;
    //定义一个Vector3Int 数组
    public Vector3Int[] hexCoordinates = new []
    {
        new Vector3Int()
    };
    
    //Edge Length of every Grid
    public float size;
    private static float innerRadius = 0.866025f/2;
    private static float outerRadius = 1f/2;
    //列间距
    private static float ColumnLength = 2*innerRadius;
    //行间距
    private static float RowLength = 1.5f * outerRadius;
    /// <summary>
    /// 内径/外径  3^0.5 / 4 
    /// </summary>
    public const float InnerToOutter = 0.8660254037844386f;
 
    /// <summary>
    /// 外径/内径  4 / 3^0.5 
    /// </summary>
    public const float OutterToInner = 1.154700538379252f;
    [Button]
    public float3 GetPos()
    {
        float y = (hexCoordinate.y - hexCoordinate.z) * Mathf.Sqrt(3) / 2 * outerRadius;
        float x = hexCoordinate.x * 1.5f * outerRadius;
        // float x = hexCoordinate.x * 1.5f * outerRadius;
        // float y = (hexCoordinate.z-hexCoordinate.y) * 1.5f * outerRadius / 2 * 0.866025f;
        Instantiate(Circle,new Vector3(x,y,0),quaternion.identity);
        return new float3(x, y, 0);
    }
    
}
