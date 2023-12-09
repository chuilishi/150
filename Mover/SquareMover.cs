using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMover : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float stepLength = 2;
    [SerializeField] private float edge = 4;

    private Vector2[] movePositions = new Vector2[4];
    private Vector2 target;


    //逆时针开关
    [SerializeField] private bool ni;

    //初始位置

    private void Start()
    {
        for (int i = 0; i < movePositions.Length; i++)
        {
            movePositions[i] = new Vector2(0, 0);
        }

        transform.position = new Vector2(parent.position.x - edge / 2, parent.position.y - edge / 2);
        SetupPosition();
        SetupTarget();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //计算并设置四角位置
    private void SetupPosition()
    {
        //第一象限的角
        movePositions[0] = new Vector2(parent.position.x - edge / 2, parent.position.y + edge / 2);
        //第二象限的角
        movePositions[1] = new Vector2(parent.position.x + edge / 2, parent.position.y + edge / 2);
        //第四象限的角
        movePositions[2] = new Vector2(parent.position.x + edge / 2, parent.position.y - edge / 2);
        //第三象限的角
        movePositions[3] = new Vector2(parent.position.x - edge / 2, parent.position.y - edge / 2);

        //for (int i = 0; i < movePositions.Length; i++)
        //print(i + "123123" + "is" + movePositions[i] == null) ;
    }

    //移动
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, stepLength * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < 0.01f)
            SetupTarget();
    }

    private void SetupTarget()
    {
        //判断当前位置处于第几象限，然后顺时针（逆时针移动）
        if (transform.position.x < parent.position.x && transform.position.y > parent.position.y)
        {
            //目前在第一象限，下一个目标在第三象限（逆时针）或者第二象限
            if (ni)
                target = movePositions[3];
            else target = movePositions[1];
        }

        else if (transform.position.x > parent.position.x && transform.position.y > parent.position.y)
        {
            //目前在第二象限，下一个目标在第一象限（逆时针）或者第四象限
            if (ni)
                target = movePositions[0];
            else target = movePositions[2];
        }
        else if (transform.position.x < parent.position.x && transform.position.y < parent.position.y)
        {
            //目前在第三象限，下一个目标在第四象限（逆时针）或者第一象限
            if (ni)
                target = movePositions[2];
            else target = movePositions[0];
        }
        else if (transform.position.x > parent.position.x && transform.position.y < parent.position.y)
        {
            //目前在第四象限，下一个目标在第二象限（逆时针）或者第三象限
            if (ni)
                target = movePositions[1];
            else target = movePositions[3];
        }
    }
}