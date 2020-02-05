using UnityEngine;
using homehelp.Events;

namespace homehelp.Variables
{
    [CreateAssetMenu(menuName = "Variable/Float", fileName = "New Float Variable")]
    public class FloatVariable : ScriptableObject
{
    public enum GameEventType
    {
        Float,
        Void,
        FloatAndVoid
    }

    public GameEventType gameEventType;
    public GameEventFloat changedEventFloat;
    public GameEventVoid changedEventVoid;

    [SerializeField] private float value;
    public float Value
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

    public void SetValue(float value) // é necessário pois poderá ser chamado a parte depois
    {
        this.value = value;

        if (changedEventFloat != null)
        {
            switch (gameEventType)
            {
                case GameEventType.Float:
                    if (changedEventFloat != null)
                    {
                        changedEventFloat.Raise(value);
                    }
                    break;
                case GameEventType.Void:
                    if (changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                    }
                    break;
                default:
                    if (changedEventFloat != null && changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                        changedEventFloat.Raise(value);
                    }
                    break;
            }
        }
    }

    public static implicit operator float(FloatVariable variable)
    {
        return variable.Value;
    }
}
}
