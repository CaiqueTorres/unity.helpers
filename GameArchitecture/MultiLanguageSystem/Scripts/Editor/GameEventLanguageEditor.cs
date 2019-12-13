using UnityEngine;
using UnityEditor;

namespace MultiLanguageText
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GameEventLanguage))]
    public class GameEventLanguageEditor : Editor
    {
        GameEventLanguage e;

        public void OnEnable()
        {
            e = (GameEventLanguage)target;
            e.reference = null;
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

            GUILayout.Label("Value: " + e.r, boldText);
            GUILayout.Space(10);

            GUILayout.Label("Press the Raise button to simulate the event in the game");
            if (GUILayout.Button("Raise"))
                e.Raise(e.reference);

            GUILayout.Space(10);
            GUILayout.Label("Language", rightAligment);
        }
    }
}

