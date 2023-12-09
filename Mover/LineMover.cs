using UnityEngine;
using UnityEngine.Serialization;

public class LineMover : BaseMover
{
    public Vector3 endPos;
    public float speed;
    protected override void Start()
    {
        originPos = transform.position;
        base.Start();
    }
    protected override void Move()
    {
        base.Move();
        t += stepLength/100*speed;
        var parentPosition = parent.transform.position;
        transform.position = Vector2.Lerp(originPos+parentPosition, endPos+parentPosition, t);
        if(t>=1)StopMoving();
    }
}
