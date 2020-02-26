using System.IO;
using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : Editor
    {
        private int type;
        FloatVariable _floatVariable;

        public void OnEnable()
        {
            _floatVariable = (FloatVariable) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            type = GUILayout.Toolbar(type, new string[] {"Float", "Void", "Float and Void"},
                GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (type)
            {
                case 0:
                    GUILayout.Label("Value: " + _floatVariable.Value);
                    _floatVariable.gameEventType = FloatVariable.GameEventType.Float;
                    _floatVariable.changedEventFloat = (GameEventFloat) EditorGUILayout.ObjectField("Game Event Float",
                        _floatVariable.changedEventFloat, typeof(GameEventFloat), false);
                    break;
                case 1:
                    _floatVariable.gameEventType = FloatVariable.GameEventType.Void;
                    _floatVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _floatVariable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    _floatVariable.gameEventType = FloatVariable.GameEventType.FloatAndVoid;
                    GUILayout.Label("Value: " + _floatVariable.Value);
                    _floatVariable.changedEventFloat = (GameEventFloat) EditorGUILayout.ObjectField("Game Event Float",
                        _floatVariable.changedEventFloat, typeof(GameEventFloat), false);
                    _floatVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _floatVariable.changedEventVoid, typeof(GameEventVoid), false);
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
                CreateInterface(_floatVariable.name);
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
                var code = string.Concat("public interface ", "I", name, "\n{\n", "\tfloat ", "SetVariableValueProcess",
                    "(float value);", "\n}\n");
                streamWriter.Write(code);
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}