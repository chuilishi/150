using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEvent
{
    public readonly int Priority;

    public IEvent(int priority)
    {
        this.Priority = priority;
    }
}
