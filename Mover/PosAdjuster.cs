using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using JetBrains.Annotations;
using UnityEngine;
using JetBrains.Rider.Unity;

public class PosAdjuster : MonoBehaviour
{
    [Button]
    public void AdjustPos()
    {
        var parent = GetComponent<MoveBase>().GetType().GetField("parent").GetValue(GetComponent<MoveBase>()) as Transform;
        if (parent != null)
        {
            
        }
    }
}
