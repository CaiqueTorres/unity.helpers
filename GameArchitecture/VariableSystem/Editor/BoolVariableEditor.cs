using System.IO;
using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : Editor
    {
        private int type;
        private BoolVariable _boolVariable;

        public void OnEnable()
        {
            _boolVariable = (BoolVariable) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            type = GUILayout.Toolbar(type, new string[] {"Bool", "Void", "Bool and Void"},
                GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (type)
            {
                case 0:
                    GUILayout.Label("Value: " + _boolVariable.Value);
                    _boolVariable.gameEventType = BoolVariable.GameEventType.Bool;
                    _boolVariable.changedEventBool = (GameEventBool) EditorGUILayout.ObjectField("Game Event Float",
                        _boolVariable.changedEventBool, typeof(GameEventBool), false);
                    break;
                case 1:
                    _boolVariable.gameEventType = BoolVariable.GameEventType.Void;
                    _boolVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _boolVariable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    GUILayout.Label("Value: " + _boolVariable.Value);
                    _boolVariable.gameEventType = BoolVariable.GameEventType.BoolAndVoid;
                    _boolVariable.changedEventBool = (GameEventBool) EditorGUILayout.ObjectField("Game Event Float",
                        _boolVariable.changedEventBool, typeof(GameEventBool), false);
                    _boolVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _boolVariable.changedEventVoid, typeof(GameEventVoid), false);
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
                CreateInterface(_boolVariable.name);
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
                var code = string.Concat("public interface ", "I", name, "\n{\n", "\tbool ", "SetVariableValueProcess",
                    "(bool value);", "\n}\n");
                streamWriter.Write(code);
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}