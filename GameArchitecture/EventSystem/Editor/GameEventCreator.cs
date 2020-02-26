using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameEventCreator : EditorWindow
{
    private static readonly string pathToBase = "Assets/3rdParty/home.help/EventSystem/Base/";
    public static readonly string pathToType = "Assets/3rdParty/home.help/EventSystem/Types/";

    private static string _className;
    private static TextAsset _textAsset;

    private int _type;

    #region Editor window
    [MenuItem("Window/Game Event Creator")]
    public static void ShowWindow() => GetWindow<GameEventCreator>("Game Event Creator");

    private void OnGUI()
    {
        var toolbarStyle = new GUIStyle(GUI.skin.button)
        {
            fixedWidth = 200f,
            alignment = TextAnchor.MiddleLeft,
        };
        
        _type = GUILayout.Toolbar(_type, new string[] { "Asset", "String" }, toolbarStyle);
        GUILayout.Space(10);
        
        switch (_type)
        {
            case 0:
                _textAsset = (TextAsset)EditorGUILayout.ObjectField("Text file:", _textAsset, typeof(TextAsset), false);
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
                _className = _textAsset.text;
            
            Create(_className);
        }
    }
    #endregion

    #region Validation
    private static bool Validate(string className) => !string.IsNullOrEmpty(className);
    #endregion

    #region Game event creation
    public static void Create(string className)
    {
        if (Validate(className))
        {
            var path = string.Concat(pathToType, className, "/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            #region Creating game event
            var filePath = string.Concat(path, "GameEvent", className, ".cs");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var streamWriter = new StreamWriter(filePath))
            {
                string code;
                using (var streamReader = new StreamReader(string.Concat(pathToBase, "GameEventTextBase.txt")))
                {
                    code = streamReader.ReadToEnd().Replace("_className_", className);
                }
                streamWriter.Write(code);
            }
            #endregion

            #region Creating game event listener
            filePath = string.Concat(path, "EventListener", className, ".cs");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var streamWriter = new StreamWriter(filePath))
            {
                string code;
                using (var streamReader = new StreamReader(string.Concat(pathToBase, "EventListenerTextBase.txt")))
                {
                    code = streamReader.ReadToEnd().Replace("_className_", className);
                }
                streamWriter.Write(code);
            }
            #endregion

            #region Creating untiy event
            filePath = string.Concat(path, "UnityEvent", className, ".cs");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var streamWriter = new StreamWriter(filePath))
            {
                string code;
                using (var streamReader = new StreamReader(string.Concat(pathToBase, "UnityEventTextBase.txt")))
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
