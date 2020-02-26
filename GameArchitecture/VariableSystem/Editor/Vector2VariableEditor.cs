using System.IO;
using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : Editor
    {
        private int _type;
        private Vector2Variable _Vector2Variable;

        public void OnEnable()
        {
            _Vector2Variable = (Vector2Variable) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            _type = GUILayout.Toolbar(_type, new string[] {"Vector2", "Void", "Vector2 and Void"},
                GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (_type)
            {
                case 0:
                    GUILayout.Label("Value: " + _Vector2Variable.Value);
                    _Vector2Variable.gameEventType = Vector2Variable.GameEventType.Vector2;
                    _Vector2Variable.changedEventVector2 = (GameEventVector2) EditorGUILayout.ObjectField("Game Event Vector2",
                        _Vector2Variable.changedEventVector2, typeof(GameEventVector2), false);
                    break;
                case 1:
                    _Vector2Variable.gameEventType = Vector2Variable.GameEventType.Void;
                    _Vector2Variable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _Vector2Variable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    GUILayout.Label("Value: " + _Vector2Variable.Value);
                    _Vector2Variable.gameEventType = Vector2Variable.GameEventType.Vector2AndVoid;
                    _Vector2Variable.changedEventVector2 = (GameEventVector2) EditorGUILayout.ObjectField("Game Event Vector2",
                        _Vector2Variable.changedEventVector2, typeof(GameEventVector2), false);
                    _Vector2Variable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _Vector2Variable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
            }

            GUILayout.Space(10);
            EditorGUILayout.HelpBox("If you don't want to raise a game event just keep the GameEvent as None",
                MessageType.Info);

            #region Interface

            EditorGUI.BeginDisabledGroup(Application.isPlaying);

            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 200f,
                margin = new RectOffset(0, 0, 20, 10),
            };

            if (GUILayout.Button("Create interface", buttonStyle))
            {
                CreateInterface(_Vector2Variable.name);
            }
            EditorGUILayout.HelpBox("Do not create variables with white spaces", MessageType.Warning);

            EditorGUI.EndDisabledGroup();

            #endregion

#if UNITY_EDITOR
            EditorUtility.SetDirty(target);
#endif
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
                var code = string.Concat("public interface ", "I", name,"\n{\n", "\tVector2 ", "SetVariableValueProcess", "(Vector2 value);", "\n}\n");
                streamWriter.Write(code);
            }
            
            AssetDatabase.Refresh();
        }
        #endregion
    }
}