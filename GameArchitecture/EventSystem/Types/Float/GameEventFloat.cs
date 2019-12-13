using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float GameEvent", menuName = "GameEvent/Float")]
public class GameEventFloat : ScriptableObject
{
    private List<EventListenerFloat> eventListeners = new List<EventListenerFloat>();
    public float reference;

    private void Awake() { reference = 0f; }

    public void Raise(float value) 
    {
        reference = value;
        foreach (EventListenerFloat item in eventListeners) { item.OnEventRaised(value); }
    }

    public void Register(EventListenerFloat listener)
    {
        if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
    }

    public void Unregister(EventListenerFloat listener)
    {
        if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
    }
}
