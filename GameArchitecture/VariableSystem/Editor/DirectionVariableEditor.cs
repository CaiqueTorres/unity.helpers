using System.IO;
using UnityEngine;
using UnityEditor;
using homehelp.Events;

namespace homehelp.Variables
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DirectionVariable))]
    public class DirectionVariableEditor : Editor
    {
        private int _type;
        private DirectionVariable _DirectionVariable;

        public void OnEnable()
        {
            _DirectionVariable = (DirectionVariable) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Game Event Type");
            _type = GUILayout.Toolbar(_type, new string[] {"Direction", "Void", "Direction and Void"},
                GUILayout.Width(Screen.width * 0.595f));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            switch (_type)
            {
                case 0:
                    GUILayout.Label("Value: " + _DirectionVariable.Value);
                    _DirectionVariable.gameEventType = DirectionVariable.GameEventType.Direction;
                    _DirectionVariable.changedEventDirection = (GameEventDirection) EditorGUILayout.ObjectField("Game Event Direction",
                        _DirectionVariable.changedEventDirection, typeof(GameEventDirection), false);
                    break;
                case 1:
                    _DirectionVariable.gameEventType = DirectionVariable.GameEventType.Void;
                    _DirectionVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _DirectionVariable.changedEventVoid, typeof(GameEventVoid), false);
                    break;
                default:
                    GUILayout.Label("Value: " + _DirectionVariable.Value);
                    _DirectionVariable.gameEventType = DirectionVariable.GameEventType.DirectionAndVoid;
                    _DirectionVariable.changedEventDirection = (GameEventDirection) EditorGUILayout.ObjectField("Game Event Direction",
                        _DirectionVariable.changedEventDirection, typeof(GameEventDirection), false);
                    _DirectionVariable.changedEventVoid = (GameEventVoid) EditorGUILayout.ObjectField("Game Event Void",
                        _DirectionVariable.changedEventVoid, typeof(GameEventVoid), false);
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
                CreateInterface(_DirectionVariable.name);
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
                var code = string.Concat("public interface ", "I", name,"\n{\n", "\tDirection ", "SetVariableValueProcess", "(Direction value);", "\n}\n");
                streamWriter.Write(code);
            }
            
            AssetDatabase.Refresh();
        }
        #endregion
    }
}