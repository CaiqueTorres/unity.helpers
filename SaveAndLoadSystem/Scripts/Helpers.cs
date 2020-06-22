using System;
using System.IO;
using System.Linq;
using Application = UnityEngine.Application;

namespace Game.SaveAndLoadSystem.Scripts
{
    public static partial class SaveSystem
    {
        private const string DefaultDataPath = "/GameData/";
        private static readonly string FolderPath = string.Concat(Application.persistentDataPath, DefaultDataPath);

        private static void CreateGameDataFolder()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
        }
        
        public static void ResetGameData()
        {
            if (!Directory.Exists(FolderPath))
                Directory.Delete(FolderPath, true);
        }

        private static string CreateFileName(Type type) => type.ToString().Split('.').Last();
    }
}
