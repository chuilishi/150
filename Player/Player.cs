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
        movers[centerIndex].parent = GameObjectUtility.CenterGameObj;
        for (int i = 0; i < movers.Count; i++)
        {
            if (movers[i].parent!=null)
            {
                movers[i].parent = movers[centerIndex].transform;
            }
            else
            {
                movers[i].parent = GameObjectUtility.CenterGameObj;
            }
            movers[i].IsMoving = i!=centerIndex;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movers[activeIndex].IsMoving = false;
            movers[activeIndex].SetPosByPos(HexMap.GetNearestUnityPos(movers[activeIndex].transform.position));
            // Instantiate(Circle, HexMap.GetNearestUnityPos(movers[activeIndex].transform.position), quaternion.identity);
            movers[centerIndex].IsMoving = true;
            (centerIndex, activeIndex) = (activeIndex, centerIndex);
            for (int i = 0; i < movers.Count; i++)
            {
                if(i==centerIndex)continue;
                movers[i].parent = movers[centerIndex].transform;
            }
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
            mover.Move(mover.parent.transform.position,ref parentPos);
        }
    }
}
