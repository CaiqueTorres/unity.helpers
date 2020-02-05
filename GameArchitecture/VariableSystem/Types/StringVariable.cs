using UnityEngine;
using homehelp.Events;

namespace homehelp.Variables
{
    [CreateAssetMenu(menuName = "Variable/String", fileName = "New String Variable")]
    public class StringVariable : ScriptableObject
{
    public enum GameEventType
    {
        String,
        Void,
        StringAndVoid
    }

    public GameEventType gameEventType;
    public GameEventString changedEventString;
    public GameEventVoid changedEventVoid;

    [SerializeField] private string value;

    public string Value
    {
        get
        {
            return value;
        }
        set
        {
            SetValue(value);
        }
    }

    public void SetValue(string value) // é necessário pois poderá ser chamado a parte depois
    {
        this.value = value;

        if (changedEventString != null)
        {

            switch (gameEventType)
            {
                case GameEventType.String:
                    if (changedEventString != null)
                    {
                        changedEventString.Raise(value);
                    }
                    break;
                case GameEventType.Void:
                    if (changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                    }
                    break;
                default:
                    if (changedEventString != null && changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                        changedEventString.Raise(value);
                    }
                    break;
            }
        }
    }

    public static implicit operator string(StringVariable variable)
    {
        return variable.Value;
    }
}
}
