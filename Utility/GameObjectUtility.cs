using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameObjectUtility : MonoBehaviour
{
    public static Transform CenterGameObj;
    public static BaseMover BaseMoverObj;
    private void Awake()
    {
        CenterGameObj = new GameObject().transform;
        BaseMoverObj = new GameObject().AddComponent<CircleMover>();
    }
}