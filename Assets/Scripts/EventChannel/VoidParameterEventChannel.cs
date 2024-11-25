using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventChannel/VoidParameter", fileName = "VoidParameterEventChannel_")]
public class VoidParameterEventChannel : ScriptableObject
{
    private event System.Action Delegate;

    public void BroadCast()
    {
        Delegate?.Invoke();
    }

    public void AddListener(System.Action action)
    {
        Delegate += action;
    }

    public void RemoveListener(System.Action action)
    {
        Delegate -= action;
    }
}
