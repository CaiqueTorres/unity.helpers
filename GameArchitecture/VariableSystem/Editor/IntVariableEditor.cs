﻿using System.IO;
using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : Editor
    {
        private int _type;
        private IntVariable _intVariable;

        public void OnEnable()
        {
            _intVariable = (IntVariable) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            _type = GUILayout.Toolbar(_type, new string[] {"Int", "Void", "Int and Void"},
                GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (_type)
            {
                case 0:
                    GUILayout.Label("Value: " + _intVariable.Value);
                    _intVariable.gameEventType = IntVariable.GameEventType.Int;
                    _intVariable.changedEventInt = (GameEventInt) EditorGUILayout.ObjectField("Game Event Int",
                        _intVariable.changedEventInt, typeof(GameEventInt), false);
                    break;
                case 1:
                    _intVariable.gameEventType = IntVariable.GameEventType.Void;
                    _intVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _intVariable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    GUILayout.Label("Value: " + _intVariable.Value);
                    _intVariable.gameEventType = IntVariable.GameEventType.IntAndVoid;
                    _intVariable.changedEventInt = (GameEventInt) EditorGUILayout.ObjectField("Game Event Int",
                        _intVariable.changedEventInt, typeof(GameEventInt), false);
                    _intVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _intVariable.changedEventVoid, typeof(GameEventVoid), false);
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
                CreateInterface(_intVariable.name);
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
                var code = string.Concat("public interface ", "I", name, "\n{\n", "\tint ", "SetVariableValueProcess",
                    "(int value);", "\n}\n");
                streamWriter.Write(code);
            }

            AssetDatabase.Refresh();
        }

        #endregion
    }
}