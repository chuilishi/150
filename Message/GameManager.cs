using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isDraw = false;
    private bool isMove = false;
    public void Draw()
    {
        isDraw = !isDraw;
        MessageSystem.GetInstance().BroadCast<DrawMessage>(new DrawMessage(isDraw));
    }
    public void Move()
    {
        isMove = !isMove;
        MessageSystem.GetInstance().BroadCast<MovementMessage>(new MovementMessage(isMove));
    }

    public void Clear()
    {
        MessageSystem.GetInstance().BroadCast<ClearMessage>(new ClearMessage());
    }
}