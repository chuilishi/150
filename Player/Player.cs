using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public BaseMover Circle;
    public List<BaseMover> movers;
    /// <summary>
    /// movers中的哪个物体是root obj
    /// </summary>
    public int centerIndex = 0;
    /// <summary>
    /// 目前正在选择的物体(预备成为下一个root)
    /// </summary>
    public int activeIndex = 1;
    
    //可达的地方会有的指示器
    [SerializeField] private GameObject accessibleIndicatorObj;
    private SpriteRenderer[] _accessibleIndicatorObjs;

    private void Start()
    {
        // //生成10个指示器,全部disable
        // _accessibleIndicatorObjs = Enumerable.Repeat(accessibleIndicatorObj, 10)
        //     .Select(o => { GetComponent<SpriteRenderer>().enabled = false;
        //         return GetComponent<SpriteRenderer>();
        //     }).ToArray();
        movers[centerIndex].parent = GameObjectUtility.BaseMoverObj;
        for (int i = 0; i < movers.Count; i++)
        {
            if (movers[i].parent!=GameObjectUtility.BaseMoverObj)
            {
                movers[i].parent = movers[centerIndex];
            }
            else
            {
                movers[i].parent = GameObjectUtility.BaseMoverObj;
            }
            movers[i].IsMoving = i!=centerIndex;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(movers[activeIndex].GetNearestT()-movers[activeIndex].t>0.2f)return;
            movers[activeIndex].IsMoving = false;
            //顺序很重要,移动的mover的t必须先确定
            movers[activeIndex].SetPos(movers[activeIndex].GetNearestT());
            (centerIndex, activeIndex) = (activeIndex, centerIndex);
            for (int i = 0; i < movers.Count; i++)
            {
                if (i == centerIndex)
                {
                    movers[i].parent = GameObjectUtility.BaseMoverObj;
                    continue;
                }
                movers[i].parent = movers[centerIndex];
            }
            movers[activeIndex].IsMoving = true;
        }
    }
    private void FixedUpdate()
    {
        var parentPos = Vector3.zero;
        parentPos = movers[centerIndex].transform.position;
        // Vector3 pParent = parentPos;
        // Vector3 qParent = parentPos;
        // for (int p = centerIndex, q = centerIndex; p >= 0 || q < movers.Count; --p, ++q)
        // {
        //     if (p >= 0)
        //     {
        //         movers[p].Move(pParent,ref pParent);
        //     }
        //     if (q < movers.Count)
        //     {
        //         movers[q].Move(qParent,ref qParent);
        //     }
        // }
        foreach (var mover in movers)
        {
            mover.Move();
        }
    }
}
