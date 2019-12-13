using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventVoid))]
public class GameEventVoidEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUI.enabled = Application.isPlaying;

        GUILayout.Label("Press the Raise button to simulate the event in the game");

        GameEventVoid e = target as GameEventVoid;
        if (GUILayout.Button("Raise"))
            e.Raise();

        var rightAligment = new GUIStyle(GUI.skin.label);
        rightAligment.alignment = TextAnchor.LowerRight;

        GUILayout.Space(10);
        GUILayout.Label("Void", rightAligment);
    }
}
