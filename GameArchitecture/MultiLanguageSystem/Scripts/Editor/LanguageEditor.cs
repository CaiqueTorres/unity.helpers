using UnityEditor;

namespace MultiLanguageText 
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Language))]
    public class LanguageEditor : Editor
    {
        public Language language;

        private void OnEnable()
        {
            language = (Language)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox("This is a Language, you can use it to change the game language.", MessageType.Info);
        }
    }
}


