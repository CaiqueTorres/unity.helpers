using UnityEngine;

namespace homehelp.Events
{
    public class EventListenerString : MonoBehaviour
{
    public GameEventString gameEvent;
    public UnityEventString response;

    public void OnEnable()
    {
        if (gameEvent != null) gameEvent.Register(this);
    }
    public void OnDisable()
    {
        if (gameEvent != null) gameEvent.Unregister(this);
    }

    public void OnEventRaised(string value) { response.Invoke(value); }
}
}
