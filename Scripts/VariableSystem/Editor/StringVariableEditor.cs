using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(StringVariable))]
public class StringVariableEditor : Editor
{
    private int type;
    StringVariable sv;

    public void OnEnable()
    {
        sv = (StringVariable)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Game Event Type");
        type = GUILayout.Toolbar(type, new string[] { "String", "Void", "String and Void"}, GUILayout.Width(Screen.width * 0.595f));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        
        switch (type)
        {
            case 0:
                GUILayout.Label("Value: " + sv.Value);
                sv.gameEventType = StringVariable.GameEventType.String;
                sv.changedEventString = (GameEventString)EditorGUILayout.ObjectField("Game Event String", sv.changedEventString, typeof(GameEventString), false);
                break;
            case 1:
                sv.gameEventType = StringVariable.GameEventType.Void;
                sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                break;
            default:
                sv.gameEventType = StringVariable.GameEventType.StringAndVoid;
                GUILayout.Label("Value: " + sv.Value);
                sv.changedEventString = (GameEventString)EditorGUILayout.ObjectField("Game Event String", sv.changedEventString, typeof(GameEventString), false);
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
