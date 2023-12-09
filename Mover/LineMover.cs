using UnityEngine;
using UnityEngine.Serialization;

public class LineMover : BaseMover
{
    public Vector3 endPos;
    public float speed;
    protected override void Start()
    {
        originPos = transform.position;
    }
    public override void Move(Vector3 parentPos, ref Vector3 outPos)
    {
        if (!IsMoving)
        {
            return;
        }
        t += stepLength/100*speed;
        outPos = transform.position = (Vector2)parentPos + Vector2.Lerp(originPos, endPos, t);
        if(t>=1)
        {
            t = 1;
            IsMoving = false;
        }
    }
}
