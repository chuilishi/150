using System;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public class Vector3Obj
{
    public Vector3 value;
}

public class MainMover : MonoBehaviour
{
    private Vector3Obj pos;
    
    public delegate void MyDelegate(Vector3Obj t); 
    public event MyDelegate FixedUpdateCallback = obj => {};
    
    private void Start()
    {
        pos = new Vector3Obj();
    }
    
    protected virtual void FixedUpdate()
    {
        
        FixedUpdateCallback.Invoke(pos);
    }
}
