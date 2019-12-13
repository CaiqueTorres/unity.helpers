using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEventBool))]
public class GameEventBoolEditor : Editor
{
    GameEventBool e;
    public void OnEnable()
    {
        e = (GameEventBool)target;
        e.reference = false;
    }
    public override void OnInspectorGUI()
    {
        var rightAligment = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.LowerRight
        };

        var boldText = new GUIStyle(GUI.skin.label)
        {
            fontStyle = FontStyle.Bold
        };

        GUI.enabled = Application.isPlaying;

        GUILayout.Label("Value: " + e.reference, boldText);
        GUILayout.Space(10);

        GUILayout.Label("Press the Raise button to simulate the event in the game");
        if (GUILayout.Button("Raise"))
            e.Raise(e.reference);

        GUILayout.Space(10);
        GUILayout.Label("Bool", rightAligment);
    }
}
