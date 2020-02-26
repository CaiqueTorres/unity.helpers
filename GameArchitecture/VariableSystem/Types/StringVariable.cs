using System;
using UnityEngine;
using homehelp.Events;

namespace homehelp.Variables
{
    [CreateAssetMenu(menuName = "Variable/String", fileName = "New String Variable")]
    public class StringVariable : ScriptableObject
    {
        public Func<string, string> doWhenSetVariable = null;
        
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
            get { return value; }
            set { SetValue(value); }
        }

        public void SetValue(string value) // é necessário pois poderá ser chamado a parte depois
        {
            this.value = (null == doWhenSetVariable) ? value : doWhenSetVariable.Invoke(value);

            if (changedEventString == null) 
                return;
            
            switch (gameEventType)
            {
                case GameEventType.String:
                    if (changedEventString != null)
                    {
                        changedEventString.Raise(this.value);
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
                        changedEventString.Raise(this.value);
                    }

                    break;
            }
        }

        public static implicit operator string(StringVariable variable)
        {
            return variable.Value;
        }
    }
}