using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannel/OneParameter", fileName = "OneParameterEventChannel_")]
public class OneParameterEventChannel<T> : ScriptableObject
{
    private event System.Action<T> Delegate;

    public void BroadCast(T obj)
    {
        Delegate?.Invoke(obj);
    }

    public void AddListener(System.Action<T> action)
    {
        Delegate += action;
    }

    public void RemoveListener(System.Action<T> action)
    {
        Delegate -= action;
    }
}
