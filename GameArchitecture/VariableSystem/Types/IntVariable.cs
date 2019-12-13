using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int", fileName = "New Int Variable")]
public class IntVariable : ScriptableObject
{
    public enum GameEventType
    {
        Int,
        Void,
        IntAndVoid
    }

    public GameEventType gameEventType;
    public GameEventInt changedEventInt;
    public GameEventVoid changedEventVoid;

    private int value;
    public int Value
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

    public void SetValue(int value) // é necessário pois poderá ser chamado a parte depois
    {
        this.value = value;

        if (changedEventInt != null)
        {

            switch (gameEventType)
            {
                case GameEventType.Int:
                    if (changedEventInt != null)
                    {
                        changedEventInt.Raise(value);
                    }
                    break;
                case GameEventType.Void:
                    if (changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                    }
                    break;
                default:
                    if (changedEventInt != null && changedEventVoid != null)
                    {
                        changedEventVoid.Raise();
                        changedEventInt.Raise(value);
                    }
                    break;
            }
        }
    }

    public static implicit operator int(IntVariable variable)
    {
        return variable.Value;
    }
}
