using UnityEngine;
using System.Collections.Generic;

namespace homehelp.Events
{
    [CreateAssetMenu(fileName = "New Bool GameEvent", menuName = "GameEvent/Bool")]
    public class GameEventBool : ScriptableObject
    {
        private readonly List<EventListenerBool> _eventListeners = new List<EventListenerBool>();

        [HideInInspector] public bool value;
        public bool simulateValue;

        private void Awake()
        {
            value = false;
        }

        public void Raise(bool value)
        {
            this.value = value;
            foreach (var item in _eventListeners)
            {
                item.OnEventRaised(value);
            }
        }

        public void Register(EventListenerBool listener)
        {
            if (!_eventListeners.Contains(listener))
            {
                _eventListeners.Add(listener);
            }
        }

        public void Unregister(EventListenerBool listener)
        {
            if (_eventListeners.Contains(listener))
            {
                _eventListeners.Remove(listener);
            }
        }
    }
}