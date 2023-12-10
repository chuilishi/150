using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameObjectUtility : MonoBehaviour
{
    public static Transform CenterGameObj;
    public static GameObject BaseMoverObj;
    private void Awake()
    {
        CenterGameObj = new GameObject().transform;
        BaseMoverObj = new GameObject();
        BaseMoverObj.AddComponent<BaseMover>();
    }
}