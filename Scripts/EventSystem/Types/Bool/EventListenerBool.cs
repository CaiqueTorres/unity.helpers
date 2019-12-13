using UnityEngine;

public class EventListenerBool : MonoBehaviour
{
    public GameEventBool gameEvent;
    public UnityEventBool response;

    public void OnEnable()
    {
        if (gameEvent != null) gameEvent.Register(this);
    }
    public void OnDisable()
    {
        if (gameEvent != null) gameEvent.Unregister(this);
    }

    public void OnEventRaised(bool value) { response.Invoke(value); }
}
