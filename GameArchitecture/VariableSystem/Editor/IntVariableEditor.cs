using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(IntVariable))]
public class IntVariableEditor : Editor
{
    private int type;
    IntVariable sv;

    public void OnEnable()
    {
        sv = (IntVariable)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Game Event Type");
        type = GUILayout.Toolbar(type, new string[] { "Int", "Void", "Int and Void"}, GUILayout.Width(Screen.width * 0.595f));
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        switch (type)
        {
            case 0:
                GUILayout.Label("Value: " + sv.Value);
                sv.gameEventType = IntVariable.GameEventType.Int;
                sv.changedEventInt = (GameEventInt)EditorGUILayout.ObjectField("Game Event Int", sv.changedEventInt, typeof(GameEventInt), false);
                break;
            case 1:
                sv.gameEventType = IntVariable.GameEventType.Void;
                sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                break;
            default:
                GUILayout.Label("Value: " + sv.Value);
                sv.gameEventType = IntVariable.GameEventType.IntAndVoid;
                sv.changedEventInt = (GameEventInt)EditorGUILayout.ObjectField("Game Event Int", sv.changedEventInt, typeof(GameEventInt), false);
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
