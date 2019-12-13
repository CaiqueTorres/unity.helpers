using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void GameEvent", menuName = "GameEvent/Void")]
public class GameEventVoid : ScriptableObject
{
    private List<EventListenerVoid> eventListeners = new List<EventListenerVoid>();

    public void Raise()
    {
        for (int i = 0; i <eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void Register(EventListenerVoid listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void Unregister(EventListenerVoid listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
