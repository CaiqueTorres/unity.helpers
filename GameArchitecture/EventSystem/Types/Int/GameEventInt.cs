using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int GameEvent", menuName = "GameEvent/Int")]
public class GameEventInt : ScriptableObject
{
    private List<EventListenerInt> eventListeners = new List<EventListenerInt>();
    public int reference;

    private void Awake() { reference = 0; }

    public void Raise(int value)
    {
        reference = value;
        foreach  (EventListenerInt item in eventListeners) { item.OnEventRaised(value); }
    }

    public void Register(EventListenerInt listener)
    {
        if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
    }

    public void Unregister(EventListenerInt listener)
    {
        if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
    }
}
