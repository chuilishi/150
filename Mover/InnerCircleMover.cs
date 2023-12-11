using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerCircleMover : CircleMover
{
    protected override void Start()
    {
        base.Start();
        radius = radius * HexMap.outerRadius*3;
    }

    public override Vector3 GetPos(float curT)
    {
        return new Vector3(Mathf.Cos(t/3*Mathf.PI) * radius, Mathf.Sin(t/3*Mathf.PI) * radius);
    }
}
