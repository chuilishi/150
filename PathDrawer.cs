using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour,ISubscriber<DrawMessage>,ISubscriber<ClearMessage>
{
    public float frequency = 0.2f;
    private LineRenderer lr;
    private int pointCount = 0;
    public bool isDrawing = false;
    public float width = 0.2f;
    private IEnumerator drawCo;
    private List<GameObject> lines;
    private int linesCount = 0;
    private void Start()
    {
        Subscribe();
        var lr = GetEmptyLine();
        lines = new List<GameObject>();
        lines.Add(lr.gameObject);
        StartCoroutine(Draw());
    }
    IEnumerator Draw()
    {
        while (true)
        {
            if (isDrawing)
            {
                AddLinePoint();
            }
            yield return new WaitForSeconds(frequency);
        }
    }
    public void StartToDraw()
    {
        isDrawing = true;
    }

    private LineRenderer GetEmptyLine()
    {
        var child = Instantiate(new GameObject(),transform);
        lr = child.AddComponent<LineRenderer>();
        lr.startWidth = width;
        lr.positionCount = 0;
        linesCount++;
        return lr;
    }
    
    public void EndToDraw()
    {
        isDrawing = false;
        var lr = GetEmptyLine();
        lines.Add(lr.gameObject);
        linesCount++;
        pointCount = 0;
    }
    public void AddLinePoint()
    {
        lr.positionCount = pointCount + 1;
        lr.SetPosition(pointCount,transform.position);
        pointCount++;
    }
    public void ClearPath()
    {
        linesCount = 0;
        var lr = GetEmptyLine();
        foreach (var line in lines)
        {
            Destroy(line);
        }
        lines.Clear();
        lines.Add(lr.gameObject);
        lr.SetPositions(Array.Empty<Vector3>());
        pointCount = 0;
    }
    public void Subscribe()
    {
        MessageSystem.GetInstance().Subscribe<DrawMessage>(this);
        MessageSystem.GetInstance().Subscribe<ClearMessage>(this);
    }

    public void OnMessage(ClearMessage message)
    {
        ClearPath();
    }

    public void OnMessage(DrawMessage message)
    {
        if(message.Start)StartToDraw();
        else EndToDraw();
    }
}
