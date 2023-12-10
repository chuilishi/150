using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int activeIndex = 1;
    //可达的地方会有的指示器
    [SerializeField] private GameObject accessibleIndicatorObj;
    private SpriteRenderer[] _accessibleIndicatorObjs;

    private void Start()
    {
        //生成10个指示器,全部disable
        _accessibleIndicatorObjs = Enumerable.Repeat(accessibleIndicatorObj, 10)
            .Select(o => { GetComponent<SpriteRenderer>().enabled = false;
                return GetComponent<SpriteRenderer>();
            }).ToArray();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movers[activeIndex].IsMoving = false;
            movers[activeIndex].SetPos(HexMap.GetNearestUnityPos(movers[activeIndex].transform.position));
            movers[centerIndex].IsMoving = true;
            (centerIndex, activeIndex) = (activeIndex, centerIndex);
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
