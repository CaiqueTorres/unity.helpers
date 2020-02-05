using System.IO;
using UnityEngine;
using UnityEditor;

namespace homehelp.Events
{
    [CustomEditor(typeof(GameEventVoid))]
    public class GameEventVoidEditor : Editor
    {
        
        private GameEventVoid _gameEventVoid;

        private void OnEnable()
        {
            _gameEventVoid = (GameEventVoid) target;
        }

        public override void OnInspectorGUI()
        {
            #region Interface
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            
            EditorGUILayout.HelpBox("Do not create game events with white spaces", MessageType.Warning);

            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 200f,
                margin = new RectOffset(0,0,10,20),
            };

            if (GUILayout.Button("Create interface", buttonStyle))
            {
                CreateInterface(_gameEventVoid.name);
            }
            EditorGUI.EndDisabledGroup();
            #endregion
            
            #region Event control
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            GUILayout.Label("Press the Raise button to simulate the event in the game");

            if (GUILayout.Button("Raise"))
                _gameEventVoid.Raise();

            var rightAlignment = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.LowerRight
            };

            GUILayout.Space(10);
            GUILayout.Label("Void", rightAlignment);
            EditorGUI.EndDisabledGroup();
            #endregion
        }

        #region Interface creation
        /// <summary>
        /// O game event void será o único diferente devido
        /// a não poder passar parametros
        /// </summary>
        public static void CreateInterface(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var newFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            
            using (var streamWriter = new StreamWriter(newFilePath))
            {
                var code = string.Concat("public interface ", "I", name,"\n{\n", "\tvoid ", name, "();", "\n}\n");
                streamWriter.Write(code);
            }
            
            AssetDatabase.Refresh();
        }
        #endregion
    }
}