using UnityEngine;
using System.Collections.Generic;

namespace homehelp.Events
{
    [CreateAssetMenu(fileName = "New Float GameEvent", menuName = "GameEvent/Float")]
    public class GameEventFloat : ScriptableObject
    {
        private readonly List<EventListenerFloat> _eventListeners = new List<EventListenerFloat>();

        [HideInInspector] public float value;
        public float simulateValue;

        private void Awake()
        {
            value = 0f;
        }

        public void Raise(float value)
        {
            this.value = value;
            foreach (var item in _eventListeners)
            {
                item.OnEventRaised(value);
            }
        }

        public void Register(EventListenerFloat listener)
        {
            if (!_eventListeners.Contains(listener))
            {
                _eventListeners.Add(listener);
            }
        }

        public void Unregister(EventListenerFloat listener)
        {
            if (_eventListeners.Contains(listener))
            {
                _eventListeners.Remove(listener);
            }
        }
    }
}