using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public List<BaseMover> movers;
    /// <summary>
    /// movers中的哪个物体是root obj
    /// </summary>
    public int centerIndex;
    /// <summary>
    /// 目前正在选择的物体(预备成为下一个root)
    /// </summary>
    public int activeIndex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < movers.Count; i++)
            {
                if (i < centerIndex)
                {
                    movers[i].parent = movers[i + 1];
                }
                else if (i == centerIndex)
                {
                    movers[i].parent = GameObjectUtility.BaseMoverObj;
                }
                else
                {
                    
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
