using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : Editor
    {
        private int type;
        BoolVariable sv;

        public void OnEnable()
        {
            sv = (BoolVariable)target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            type = GUILayout.Toolbar(type, new string[] { "Bool", "Void", "Bool and Void" }, GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (type)
            {
                case 0:
                    GUILayout.Label("Value: " + sv.Value);
                    sv.gameEventType = BoolVariable.GameEventType.Bool;
                    sv.changedEventBool = (GameEventBool)EditorGUILayout.ObjectField("Game Event Float", sv.changedEventBool, typeof(GameEventBool), false);
                    break;
                case 1:
                    sv.gameEventType = BoolVariable.GameEventType.Void;
                    sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    GUILayout.Label("Value: " + sv.Value);
                    sv.gameEventType = BoolVariable.GameEventType.BoolAndVoid;
                    sv.changedEventBool = (GameEventBool)EditorGUILayout.ObjectField("Game Event Float", sv.changedEventBool, typeof(GameEventBool), false);
                    sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                    break;
            }

            GUILayout.Space(10);
            EditorGUILayout.HelpBox("If you don't want to raise a game event just keep the GameEvent as None", MessageType.Info);

    #if UNITY_EDITOR
            EditorUtility.SetDirty(target);
    #endif
        }
    }
}
