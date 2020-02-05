using System.IO;
using UnityEngine;
using UnityEditor;

public class VariableCreator : EditorWindow
{
    private static readonly string pathToBase = "Assets/3rdParty/home.help/VariableSystem/Base/";
    private static readonly string pathToType = "Assets/3rdParty/home.help/VariableSystem/Types/";

    private static string _className;
    private static TextAsset _textAsset;

    #region Editor window
    [MenuItem("Window/Variable Creator")]
    public static void ShowWindow() => GetWindow<VariableCreator>("Variable Creator");

    private void OnGUI()
    {
        GUILayout.Space(10);
        _textAsset = (TextAsset) EditorGUILayout.ObjectField("Text file:", _textAsset, typeof(TextAsset),false);

        var btnLayout = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 120f,
            margin = new RectOffset(4, 0, 10, 0),
        };

        if (GUILayout.Button("Create", btnLayout))
        {
            if (_textAsset == null)
                return;
            
            _className = _textAsset.name;
            Create(_className);
        }
    }
    #endregion

    #region Validation
    private static bool Validate(string className) => !string.IsNullOrEmpty(className);
    #endregion

    #region Varible creation
    public static void Create(string className)
    {
        if (Validate(className))
        {
            #region Creating variable
            var filePath = string.Concat(pathToType, className, "Variable", ".cs");

            if (!File.Exists(string.Concat(GameEventCreator.pathToType, "GameEvent", className, ".cs")))
            {
                GameEventCreator.Create(className);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var streamWriter = new StreamWriter(filePath))
            {
                string code;
                using (var streamReader = new StreamReader(string.Concat(pathToBase, "VariableTextBase.txt")))
                {
                    code = streamReader.ReadToEnd().Replace("_className_", className);
                }
                streamWriter.Write(code);
            }
            #endregion

            AssetDatabase.Refresh();
        }
    }
    #endregion
}
