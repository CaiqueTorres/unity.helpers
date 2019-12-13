using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventInt))]
public class GameEventIntEditor : Editor
{
    GameEventInt e;
    public void OnEnable()
    {
        e = (GameEventInt)target;
        e.reference = 0;
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
        GUILayout.Label("Int", rightAligment);
    }
}
