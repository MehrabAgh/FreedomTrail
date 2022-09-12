using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Observer<T> where T : Object
{ 
    public List<UnityAction<T>> observers = new List<UnityAction<T>>();

    public void Add(UnityAction<T> newItem)
    {
      if(!observers.Contains(newItem))  
            observers.Add(newItem);
    }

    public void Remove(UnityAction<T> item)
    {
        if (!observers.Contains(item))
            observers.Remove(item);
    }

    public void RemoveAt(int index)
    {
        if (observers.Count >= index)
            observers.RemoveAt(index);
    }

    public void Notify(T value)
    {
        for (int i = 0; i < observers.Count; i++)
            observers[i](value);
    }
}
