using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEventFloat))]
public class GameEventFloatEditor : Editor
{
    GameEventFloat e;
    public void OnEnable()
    {
        e = (GameEventFloat)target;
        e.reference = 0f;
    }
    public override void OnInspectorGUI()
    {
        var rightAligment = new GUIStyle(GUI.skin.label);
        rightAligment.alignment = TextAnchor.LowerRight;

        var boldText = new GUIStyle(GUI.skin.label);
        boldText.fontStyle = FontStyle.Bold;

        GUI.enabled = Application.isPlaying;

        GUILayout.Label("Value: " + e.reference, boldText);
        GUILayout.Space(10);

        GUILayout.Label("Press the Raise button to simulate the event in the game");
        if (GUILayout.Button("Raise"))
            e.Raise(e.reference);

        GUILayout.Space(10);
        GUILayout.Label("Float", rightAligment);
    }
}

