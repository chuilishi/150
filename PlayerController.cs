using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private float x = 0;
    private float y = 0;
    private bool isMoving = false;

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x == 0 && y == 0)
        {
            if(isMoving)
            {
                MessageSystem.GetInstance().BroadCast<DrawMessage>(new DrawMessage(true));
                isMoving = false;
            }
        }
        else
        {
            if(!isMoving)
            {
                MessageSystem.GetInstance().BroadCast<DrawMessage>(new DrawMessage(false));
                isMoving = true;
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(x, y, 0)*speed);
    }
}