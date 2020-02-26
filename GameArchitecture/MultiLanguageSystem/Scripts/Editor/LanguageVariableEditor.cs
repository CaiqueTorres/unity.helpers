using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace MultiLanguageText
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LanguageVariable))]
    public class LanguageVariableEditor : Editor
    {
        private int type;
        LanguageVariable sv;

        public void OnEnable()
        {
            sv = (LanguageVariable)target;
        }

        public override void OnInspectorGUI()
        {
            sv.Value = (Language)EditorGUILayout.ObjectField("Value", sv.Value, typeof(Language), false);
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            type = GUILayout.Toolbar(type, new string[] { "Language", "Void", "Language and Void" }, GUILayout.Width(Screen.width * 0.58f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            switch (type)
            {
                case 0:
                    GUILayout.Label("Value: " + sv.Value.languageName);
                    sv.gameEventType = LanguageVariable.GameEventType.Language;
                    sv.changedEventLanguage = (GameEventLanguage)EditorGUILayout.ObjectField("Game Event Language", sv.changedEventLanguage, typeof(GameEventLanguage), false);
                    break;
                case 1:
                    sv.gameEventType = LanguageVariable.GameEventType.Void;
                    sv.changedEventVoid = (GameEventVoid)EditorGUILayout.ObjectField("Game Event Void", sv.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    sv.gameEventType = LanguageVariable.GameEventType.LanguageAndVoid;
                    GUILayout.Label("Value: " + sv.Value.languageName);
                    sv.changedEventLanguage = (GameEventLanguage)EditorGUILayout.ObjectField("Game Event Language", sv.changedEventLanguage, typeof(GameEventLanguage), false);
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
