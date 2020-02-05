using System.Collections.Generic;
using UnityEngine;

namespace homehelp.Events
{
    [CreateAssetMenu(fileName = "New Void GameEvent", menuName = "GameEvent/Void")]
    public class GameEventVoid : ScriptableObject
    {
        private readonly List<EventListenerVoid> _eventListeners = new List<EventListenerVoid>();

        public void Raise()
        {
            for (int i = 0; i < _eventListeners.Count; i++)
            {
                _eventListeners[i].OnEventRaised();
            }
        }

        public void Register(EventListenerVoid listener)
        {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void Unregister(EventListenerVoid listener)
        {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}