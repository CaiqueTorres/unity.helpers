using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bool GameEvent", menuName = "GameEvent/Bool")]
public class GameEventBool : ScriptableObject
{
    private List<EventListenerBool> eventListeners = new List<EventListenerBool>();
    public bool reference;

    public void Raise(bool value)
    {
        reference = value;
        foreach (EventListenerBool item in eventListeners) { item.OnEventRaised(value); }
    }

    public void Register(EventListenerBool listener)
    {
        if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
    }

    public void Unregister(EventListenerBool listener)
    {
        if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
    }
}
