using UnityEngine;
using System.Collections.Generic;

namespace homehelp.Events
{
    [CreateAssetMenu(fileName = "New String GameEvent", menuName = "GameEvent/String")]
    public class GameEventString : ScriptableObject
    {
        private readonly List<EventListenerString> _eventListeners = new List<EventListenerString>();

        [HideInInspector] public string value;
        public string simulateValue;

        private void Awake()
        {
            value = "null";
        }

        public void Raise(string value)
        {
            this.value = value;
            foreach (var item in _eventListeners)
            {
                item.OnEventRaised(value);
            }
        }

        public void Register(EventListenerString listener)
        {
            if (!_eventListeners.Contains(listener))
            {
                _eventListeners.Add(listener);
            }
        }

        public void Unregister(EventListenerString listener)
        {
            if (_eventListeners.Contains(listener))
            {
                _eventListeners.Remove(listener);
            }
        }
    }
}