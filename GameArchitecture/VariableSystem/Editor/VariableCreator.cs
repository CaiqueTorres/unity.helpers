using System.IO;
using UnityEngine;
using UnityEditor;

public class VariableCreator : EditorWindow
{
    private static readonly string pathToBase = "Assets/3rdParty/home.help/VariableSystem/Base/";
    private static readonly string pathToType = "Assets/3rdParty/home.help/VariableSystem/Types/";
    private static readonly string pathToEditor = "Assets/3rdParty/home.help/VariableSystem/Editor/";

    private static string _className;
    private static TextAsset _textAsset;

    private int _type;
    
    #region Editor window
    [MenuItem("Window/Variable Creator")]
    public static void ShowWindow() => GetWindow<VariableCreator>("Variable Creator");

    private void OnGUI()
    {
        var toolbarStyle = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 200f,
            alignment = TextAnchor.MiddleLeft,
        };
        
        _type = GUILayout.Toolbar(_type, new string[] { "Asset", "String"}, toolbarStyle);
        GUILayout.Space(10f);
        
        switch (_type)
        {
            case 0:
                _textAsset = (TextAsset) EditorGUILayout.ObjectField("Text file:", _textAsset, typeof(TextAsset),false);
                break;
            case 1:
                _className = EditorGUILayout.TextField("Text:", _className);
                break;
        }
    
        GUILayout.Space(10);

        var btnLayout = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 120f,
            margin = new RectOffset(4, 0, 10, 0),
        };

        if (GUILayout.Button("Create", btnLayout))
        {
            if (_type == 0)
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
            
            #region Creating variable editor

            var filePathEditor = string.Concat(pathToEditor, className, "Variable", "Editor", ".cs");
            if (File.Exists(filePathEditor))
            {
                File.Delete(filePathEditor);
            }
            
            using (var streamWriter = new StreamWriter(filePathEditor))
            {
                string code;
                using (var streamReader = new StreamReader(string.Concat(pathToBase, "VariableEditorTextBase.txt")))
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
