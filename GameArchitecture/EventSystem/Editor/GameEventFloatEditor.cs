using System.IO;
using UnityEditor;
using UnityEngine;

namespace homehelp.Events
{
    [CustomEditor(typeof(GameEventFloat))]
    public class GameEventFloatEditor : Editor
    {
        private GameEventFloat _gameEventFloat;

        public void OnEnable()
        {
            _gameEventFloat = (GameEventFloat) target;
            _gameEventFloat.value = 0f;
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
                CreateInterface(_gameEventFloat.name);
            }
            EditorGUI.EndDisabledGroup();
            #endregion
            
            #region Event control            
            var rightAlignment = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.LowerRight
            };

            var boldText = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold
            };

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            GUILayout.Label("Value: " + _gameEventFloat.value, boldText);

            var so = new SerializedObject(_gameEventFloat);
            var sp = so.FindProperty("simulateValue");
            if (sp != null)
            {
                EditorGUILayout.PropertyField(sp);
                so.ApplyModifiedProperties();
                GUILayout.Space(10);
            }

            GUILayout.Label("Press the Raise button to simulate the event in the game");
            if (GUILayout.Button("Raise"))
                _gameEventFloat.Raise(_gameEventFloat.simulateValue);

            GUILayout.Space(10);
            GUILayout.Label("Float", rightAlignment);
            
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
                var code = string.Concat("public interface ", "I", name,"\n{\n", "\tvoid ", name, "(float value);", "\n}\n");
                streamWriter.Write(code);
            }
            
            AssetDatabase.Refresh();
        }
        #endregion
    }
}