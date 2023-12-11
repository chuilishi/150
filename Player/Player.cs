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
        #region 对parent初始化

        for (int i = 0; i < movers.Count; i++)
        {
            if (movers[i].parent == null)
            {
                if (i == centerIndex)
                {
                    movers[i].parent = GameObjectUtility.BaseMoverObj;
                    GameObjectUtility.BaseMoverObj.childMovers.Add(movers[i]);
                }
                else
                {
                    movers[i].parent = movers[centerIndex];
                    movers[centerIndex].childMovers.Add(movers[i]);
                }
            }
            else
            {
                movers[i].parent.childMovers.Add(movers[i]);
            }
            movers[i].IsMoving = i!=centerIndex;
        }
        //初始化
        foreach (var mover in movers)
        {
            mover.SetParent(mover.parent);
        }
        #endregion
        
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
        foreach (var mover in movers)
        {
            mover.hasMoved = false;
        }
        movers[centerIndex].Move();
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
