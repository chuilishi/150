using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject circle;
    [Button]
    public void Create(Vector3 coordinate)
    {
        Instantiate(circle,HexMap.GetUnityPos(coordinate),Quaternion.identity);
    }
}
