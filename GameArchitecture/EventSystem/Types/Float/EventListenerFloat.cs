using UnityEngine;

namespace homehelp.Events
{
    public class EventListenerFloat : MonoBehaviour
{
    public GameEventFloat gameEvent;
    public UnityEventFloat response;

    public void OnEnable()
    {
        if (gameEvent != null) gameEvent.Register(this);
    }

    public void OnDisable()
    {
        if (gameEvent != null) gameEvent.Unregister(this);
    }

    public void OnEventRaised(float value) { response.Invoke(value); }
}
}
