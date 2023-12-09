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
    public int centerIndex = 0;
    /// <summary>
    /// 目前正在选择的物体(预备成为下一个root)
    /// </summary>
    public int activeIndex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            centerIndex = activeIndex;
            for (int p = centerIndex, q = centerIndex; p >= 0 || q < movers.Count; --p, ++q)
            {
                if (p >= 0)
                {
                    movers[p].parent = movers[p + 1];
                }
                if (q < movers.Count)
                {
                    movers[q].parent = movers[q - 1];
                }
            }
        }
    }
    private void FixedUpdate()
    {
        var parentPos = Vector3.zero;
        movers[centerIndex].Move(movers[centerIndex].transform.position,ref parentPos);
        Vector3 pParent = parentPos;
        Vector3 qParent = parentPos;
        for (int p = centerIndex, q = centerIndex; p >= 0 || q < movers.Count; --p, ++q)
        {
            if (p >= 0)
            {
                movers[p].Move(pParent,ref pParent);
            }
            if (q < movers.Count)
            {
                movers[q].Move(qParent,ref qParent);
            }
        }
    }
}
