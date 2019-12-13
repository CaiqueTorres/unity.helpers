using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool", fileName = "New Bool Variable")]
public class BoolVariable : ScriptableObject
{
    public enum GameEventType
    {
        Bool,
        Void,
        BoolAndVoid
    }

    public GameEventType gameEventType;
    public GameEventBool changedEventBool;
    public GameEventVoid changedEventVoid;

    private bool value;
    public bool Value
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

    public void SetValue(bool value) // é necessário pois poderá ser chamado a parte depois
    {
        this.value = value;

        if (changedEventBool != null)
        {
            switch (gameEventType)
            {
                case GameEventType.Bool:
                    if (changedEventBool != null)
                    {
                        changedEventBool.Raise(value);
                    }
                    break;
                case GameEventType.Void:
                    if (changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                    }
                    break;
                default:
                    if (changedEventBool != null && changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                        changedEventBool.Raise(value);
                    }
                    break;
            }
        }
    }

    public static implicit operator bool(BoolVariable variable)
    {
        return variable.Value;
    }
}
