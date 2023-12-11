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
        for (int i = 0; i < movers.Count; i++)
        {
            movers[i].parent = GameObjectUtility.BaseMoverObj;
            if (i!=centerIndex)
            {
                movers[i].SetParent(movers[centerIndex]);
            }
            movers[i].IsMoving = i!=centerIndex;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Hit(false);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Hit(true);
        }
    }
    private void FixedUpdate()
    {
        movers[centerIndex].Move(Vector3.zero);
    }

    private void Hit(bool direction)
    {
        if(movers[activeIndex].GetNearestT()-movers[activeIndex].t>0.2f)return;
        movers[centerIndex].direction = direction;
        movers[activeIndex].IsMoving = false;
        //顺序很重要,移动的mover的t必须先确定
        movers[activeIndex].SetPos(movers[activeIndex].GetNearestT());
        (centerIndex, activeIndex) = (activeIndex, centerIndex);
        for (int i = 0; i < movers.Count; i++)
        {
            if (i == centerIndex)
            {
                movers[i].SetParent(GameObjectUtility.BaseMoverObj);
                continue;
            }
            movers[i].SetParent(movers[centerIndex]);
            movers[i].IsMoving = true;
        }
    }
}
