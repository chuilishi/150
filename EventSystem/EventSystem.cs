using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

abstract class IRegisterations
{
    public Action OnReceives;
}

class Registerations<T>: IRegisterations
{
    public new Action<T> OnReceives = obj => { };
}
public static class EventSystem
{
    private static Dictionary<Type,IRegisterations> instance = new Dictionary<Type, IRegisterations>();
    private static List<KeyValuePair<Type,IEvent>> events = new List<KeyValuePair<Type,IEvent>>();
    public static void Register<T>(Action<T> action)
    {
        IRegisterations registerations = null;
        if (instance.TryGetValue(typeof(T), out registerations))
        {
            var reg = registerations as Registerations<T>;
            reg.OnReceives += action;
        }
        else
        {
            var reg = new Registerations<T>();
            reg.OnReceives += action;
            instance.Add(typeof(T),reg);
        }
    }
    
    public static void UnRegister<T>(Action<T> action)
    {
        if (instance.ContainsKey(typeof(T)))
        {
            var reg = instance[typeof(T)] as Registerations<T>;
            reg.OnReceives -= action;
        }
    }
    /// <summary>
    /// 实际上是将T加入队列,然后用InnerBroadCast调用
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="T"></typeparam>
    public static void BroadCast<T>(T t)
    {
        if(instance[typeof(T)]==null)return;
        events.Add(new KeyValuePair<Type, IEvent>(typeof(T),t as IEvent));
        events.Sort(Comparer<KeyValuePair<Type, IEvent>>.Create((a, b) => a.Value.Priority.CompareTo(b.Value.Priority)));
    }
    private static void InnerBroadCast<T>(T t)
    {
        while (events.Count>0)
        {
            var reg = instance[events[^1].Key];
            
        }
    }
    public static void Run()
    {
        while (true)
        {
            if(events.Count==0)continue;
            InnerBroadCast(events[^1]);
            events.RemoveAt(events.Count-1);
        }
    }
}