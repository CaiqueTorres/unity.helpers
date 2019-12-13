using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String GameEvent", menuName = "GameEvent/String")]
public class GameEventString : ScriptableObject
{
    private List<EventListenerString> eventListeners = new List<EventListenerString>();
    public string reference;

    private void Awake() { reference = "null"; }

    public void Raise(string value)
    {
        reference = value;
        foreach  (EventListenerString item in eventListeners) { item.OnEventRaised(value); }
    }

    public void Register(EventListenerString listener)
    {
        if (!eventListeners.Contains(listener)) { eventListeners.Add(listener); }
    }

    public void Unregister(EventListenerString listener)
    {
        if (eventListeners.Contains(listener)) { eventListeners.Remove(listener); }
    }
}
