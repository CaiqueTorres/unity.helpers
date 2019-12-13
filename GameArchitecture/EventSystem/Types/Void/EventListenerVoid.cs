using UnityEngine;
using UnityEngine.Events;

public class EventListenerVoid : MonoBehaviour
{
    public GameEventVoid gameEvent;
    public UnityEvent response;

    public void OnEnable()
    {
        if (gameEvent != null) gameEvent.Register(this);
    }
    public void OnDisable()
    {
        if (gameEvent !=  null) gameEvent.Unregister(this);
    }

    public void OnEventRaised() { response.Invoke(); }
}
