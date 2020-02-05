using UnityEngine;

namespace homehelp.Events
{
    public class EventListenerInt : MonoBehaviour
    {
        public GameEventInt gameEvent;
        public UnityEventInt response;

        public void OnEnable()
        {
            if (gameEvent != null) gameEvent.Register(this);
        }
        public void OnDisable()
        {
            if (gameEvent != null) gameEvent.Unregister(this);
        }

        public void OnEventRaised(int value) { response.Invoke(value); }
    }
}
