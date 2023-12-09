using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMover : MoveBase
{
    public float a = 1f;
    private float t = 0;
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        t += stepLength;
        var x = a * (2 * Mathf.Cos(t) - Mathf.Cos(2 * t));
        var y = a * (2 * Mathf.Sin(t) - Mathf.Sin(2 * t));
        transform.position = new Vector3(x, y);
    }
}
