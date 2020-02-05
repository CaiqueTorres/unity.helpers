using UnityEditor;
using UnityEngine;

public class FolderStructure : MonoBehaviour
{
	[MenuItem("Assets/Create/Create Base Folders")]
	public static void CreateBaseFolderStructure()
    {
        string guid;
        if (!AssetDatabase.IsValidFolder("Assets/3rdParty"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "3rdParty");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder("Assets/Game"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "Game");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder("Assets/EventSystem"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "GameEventSystem");
            AssetDatabase.GUIDToAssetPath(guid);
        }

        if (!AssetDatabase.IsValidFolder("Assets/Plugins"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "Plugins");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder("Assets/Sandbox"))
        {
            guid = AssetDatabase.CreateFolder("Assets", "Sandbox");
            AssetDatabase.GUIDToAssetPath(guid); 
        }
	}

	[MenuItem("Assets/Create/Create Entity Folders")]
	public static void CreateEntityFolders()
	{
		string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string guid;

        if (!AssetDatabase.IsValidFolder(folderPath + "/Art"))
        {
            guid = AssetDatabase.CreateFolder(folderPath, "Art");
            AssetDatabase.GUIDToAssetPath(guid);
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Art/Animations"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/Art", "Animations");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Art/Audio"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/Art", "Audio");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Art/Material"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/Art", "Material");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Art/Sprites"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/Art", "Sprites");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Prefabs"))
        {
            guid = AssetDatabase.CreateFolder(folderPath, "Prefabs");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Scripts"))
        {
            guid = AssetDatabase.CreateFolder(folderPath, "Scripts");
            AssetDatabase.GUIDToAssetPath(guid); 
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/Scripts/Editor"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/Scripts", "Editor");
            AssetDatabase.GUIDToAssetPath(guid);
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/ScriptableObjects"))
        {
            guid = AssetDatabase.CreateFolder(folderPath, "ScriptableObjects");
            AssetDatabase.GUIDToAssetPath(guid);
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/ScriptableObjects/GameEvents"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/ScriptableObjects", "GameEvents");
            AssetDatabase.GUIDToAssetPath(guid);
        }

        if (!AssetDatabase.IsValidFolder(folderPath + "/ScriptableObjects/Variables"))
        {
            guid = AssetDatabase.CreateFolder(folderPath + "/ScriptableObjects", "Variables");
            AssetDatabase.GUIDToAssetPath(guid);
        }
    }
}
