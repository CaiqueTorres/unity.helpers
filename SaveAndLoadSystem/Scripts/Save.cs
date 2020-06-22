using System;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game.SaveAndLoadSystem.Scripts
{
    public static partial class SaveSystem
    {
        public static void Save<T>(T data, Action<Exception> callback = null)
            where T : class
        {
            try
            {
                CreateGameDataFolder();

                var fileName = CreateFileName(typeof(T));
                var binaryFormatter = new BinaryFormatter();
                var filePath = string.Concat(FolderPath, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var file = File.Create(filePath))
                {
                    var json = JsonUtility.ToJson(data);
                    binaryFormatter.Serialize(file, json);
                }
            }
            catch (Exception exception)
            {
                callback?.Invoke(exception);
            }
        }
        
        public static void Save<T>(string fileName, T data, Action<Exception> callback = null)
            where T : class
        {
            try
            {
                CreateGameDataFolder();

                var binaryFormatter = new BinaryFormatter();
                var filePath = string.Concat(FolderPath, fileName);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var file = File.Create(filePath))
                {
                    var json = JsonUtility.ToJson(data);
                    binaryFormatter.Serialize(file, json);
                }
            }
            catch (Exception exception)
            {
                callback?.Invoke(exception);
            }
        }
        
        public static async Task SaveAsync<T>(T data, Action<Exception> callback = null)
            where T : class
        {
            await Task.Run(() =>
            {
                try
                {
                    CreateGameDataFolder();

                    var fileName = CreateFileName(typeof(T));
                    var binaryFormatter = new BinaryFormatter();
                    var filePath = string.Concat(FolderPath, fileName);

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    using (var file = File.Create(filePath))
                    {
                        var json = JsonUtility.ToJson(data);
                        binaryFormatter.Serialize(file, json);
                    }
                }
                catch (Exception exception)
                {
                    callback?.Invoke(exception);
                }
            });
        }
        
        public static async Task SaveAsync<T>(string fileName, T data, Action<Exception> callback = null)
            where T : class
        {
            await Task.Run(() =>
            {
                try
                {
                    CreateGameDataFolder();

                    var binaryFormatter = new BinaryFormatter();
                    var filePath = string.Concat(FolderPath, fileName);

                    if (File.Exists(filePath))
                        File.Delete(filePath);

                    using (var file = File.Create(filePath))
                    {
                        var json = JsonUtility.ToJson(data);
                        binaryFormatter.Serialize(file, json);
                    }
                }
                catch (Exception exception)
                {
                    callback?.Invoke(exception);
                }
            });
        }
    }
}
