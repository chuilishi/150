using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem
{
    public Dictionary<string, HashSet<ISubscriber>> m_messages = new Dictionary<string, HashSet<ISubscriber>>();
    private static MessageSystem instance = new MessageSystem();
    public static MessageSystem GetInstance()
    {
        return instance;
    }

    public void Subscribe<T>(ISubscriber subscriber)
    {
        string name = typeof(T).Name;
        if (m_messages.ContainsKey(name))
        {
            m_messages[name].Add(subscriber);
        }
        else
        {
            var newset = new HashSet<ISubscriber>();
            newset.Add(subscriber);
            m_messages.Add(name,newset);
        }
    }

    public void UnSubscribe<T>(ISubscriber subscriber)
    {
        string name = typeof(T).Name;
        if (m_messages.ContainsKey(name))
        {
            m_messages[name].Remove(subscriber);
        }
    }

    public void BroadCast<T>(T message)
    {
        string name = typeof(T).Name;
        if (!m_messages.ContainsKey(name))
        {
            m_messages.Add(name,new HashSet<ISubscriber>());
        }
        foreach (var subscriber in m_messages[name])
        {
            (subscriber as ISubscriber<T>).OnMessage(message);
        }
    }
}
