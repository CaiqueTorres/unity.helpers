using System;
using UnityEngine;
using homehelp.Events;

namespace homehelp.Variables
{
    [CreateAssetMenu(menuName = "Variable/Int", fileName = "New Int Variable")]
    public class IntVariable : ScriptableObject
    {
        public Func<int, int> doWhenSetVariable = null;

        public enum GameEventType
        {
            Int,
            Void,
            IntAndVoid
        }

        public GameEventType gameEventType;
        public GameEventInt changedEventInt;
        public GameEventVoid changedEventVoid;

        [SerializeField] private int value;
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
            this.value = (null == doWhenSetVariable) ? value : doWhenSetVariable.Invoke(value);

            if (changedEventInt == null) 
                return;
            
            switch (gameEventType)
            {
                case GameEventType.Int:
                    if (changedEventInt != null)
                    {
                        changedEventInt.Raise(this.value);
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
                        changedEventInt.Raise(this.value);
                    }
                    break;
            }
        }

        public static implicit operator int(IntVariable variable)
        {
            return variable.Value;
        }
    }
}
