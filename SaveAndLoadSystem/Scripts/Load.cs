using System;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game.SaveAndLoadSystem.Scripts
{
    public static partial class SaveSystem
    {
        public static T Load<T>(Action<Exception> callback = null)
            where T : class
        {
            try
            {
                var fileName = CreateFileName(typeof(T));
                var binaryFormatter = new BinaryFormatter();
                var filePath = string.Concat(FolderPath, fileName);

                using (var file = File.Open(filePath, FileMode.Open))
                {
                    return JsonUtility.FromJson<T>((string) binaryFormatter.Deserialize(file));
                }
            }
            catch (Exception exception)
            {
                callback?.Invoke(exception);
                return null;
            }
        }

        public static T Load<T>(string fileName, Action<Exception> callback = null)
            where T : class
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                var filePath = string.Concat(FolderPath, fileName);

                using (var file = File.Open(filePath, FileMode.Open))
                {
                    return JsonUtility.FromJson<T>((string) binaryFormatter.Deserialize(file));
                }
            }
            catch (Exception exception)
            {
                callback?.Invoke(exception);
                return null;
            }
        }

        public static async Task<T> LoadAsync<T>(Action<Exception> callback = null)
            where T : class 
        {
            return await Task.Run(() =>
            {
                try
                {
                    var fileName = CreateFileName(typeof(T));
                    var binaryFormatter = new BinaryFormatter();
                    var filePath = string.Concat(FolderPath, fileName);

                    using (var file = File.Open(filePath, FileMode.Open))
                    {
                        return JsonUtility.FromJson<T>((string) binaryFormatter.Deserialize(file));
                    }
                }
                catch (Exception exception)
                {
                    callback?.Invoke(exception);
                    return null;
                }
            });
        }

        public static async Task<T> LoadAsync<T>(string fileName, Action<Exception> callback = null)
            where T : class
        {
            return await Task.Run(() =>
            {
                try
                {
                    var binaryFormatter = new BinaryFormatter();
                    var filePath = string.Concat(FolderPath, fileName);

                    using (var file = File.Open(filePath, FileMode.Open))
                    {
                        return JsonUtility.FromJson<T>((string) binaryFormatter.Deserialize(file));
                    }
                }
                catch (Exception exception)
                {
                    callback?.Invoke(exception);
                    return null;
                }
            });
        }
    }
}
