using UnityEditor;
using UnityEngine;

namespace homehelp.Variables
{
    public class ShowVariableValue : PropertyAttribute
    {
        public readonly bool CanEdit;

        public ShowVariableValue()
        {
            CanEdit = false;
        }
        
        public ShowVariableValue(bool canEdit)
        {
            CanEdit = canEdit;
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowVariableValue))]
    public class ShowVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var canEditVariable = attribute as ShowVariableValue;
            
            var rectVariable = new Rect()
            {
                x = position.x,
                y = position.y,
                width = Screen.width * 0.8f,
                height = 17,
            };
            
            var rectValue = new Rect()
            {
                x = (position.x + Screen.width * 0.807f),
                y = position.y,
                width = (Screen.width - 6) - (position.x + Screen.width * 0.807f),
                height = 17,
            };
            
            EditorGUI.PropertyField(rectVariable, property, label);

            if (property.objectReferenceValue != null)
            {
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                EditorGUI.BeginDisabledGroup(!canEditVariable.CanEdit);
                
                    EditorGUI.PropertyField(rectValue, serializedObject.FindProperty("value"), GUIContent.none);
                    serializedObject.ApplyModifiedProperties();
                    
                EditorGUI.EndDisabledGroup();
            }
            
            EditorGUI.EndProperty();
        }
    }
#endif
}

