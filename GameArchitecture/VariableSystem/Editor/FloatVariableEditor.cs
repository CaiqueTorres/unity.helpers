using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : Editor
{
    private int type;
    FloatVariable sv;

    public void OnEnable()
    {
        sv = (FloatVariable)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Game Event Type");
        type = GUILayout.Toolbar(type, new string[] { "Float", "Void", "Float and Void" }, GUILayout.Width(Screen.width * 0.595f));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        switch (type)
        {
            case 0:
                GUILayout.Label("Value: " + sv.Value);
                sv.gameEventType = FloatVariable.GameEventType.Float;
                sv.changedEventFloat = (GameEventFloat)EditorGUILayout.ObjectField("Game Event Float", sv.changedEventFloat, typeof(GameEventFloat), false);
                break;
            case 1:
                sv.gameEventType = FloatVariable.GameEventType.Void;
                sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                break;
            default:
                sv.gameEventType = FloatVariable.GameEventType.FloatAndVoid;
                GUILayout.Label("Value: " + sv.Value);
                sv.changedEventFloat = (GameEventFloat)EditorGUILayout.ObjectField("Game Event Float", sv.changedEventFloat, typeof(GameEventFloat), false);
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
