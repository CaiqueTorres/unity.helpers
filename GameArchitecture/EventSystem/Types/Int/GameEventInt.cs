using UnityEngine;
using System.Collections.Generic;

namespace homehelp.Events
{
    [CreateAssetMenu(fileName = "New Int GameEvent", menuName = "GameEvent/Int")]
    public class GameEventInt : ScriptableObject
    {
        private readonly List<EventListenerInt> _eventListeners = new List<EventListenerInt>();

        [HideInInspector] public int value;
        public int simulateValue;

        private void Awake()
        {
            value = 0;
        }

        public void Raise(int value)
        {
            this.value = value;
            foreach (var item in _eventListeners)
            {
                item.OnEventRaised(value);
            }
        }

        public void Register(EventListenerInt listener)
        {
            if (!_eventListeners.Contains(listener))
            {
                _eventListeners.Add(listener);
            }
        }

        public void Unregister(EventListenerInt listener)
        {
            if (_eventListeners.Contains(listener))
            {
                _eventListeners.Remove(listener);
            }
        }
    }
}