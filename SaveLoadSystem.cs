using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    private const string fileName = "PlayerData.data";

    public void SavePlayerList(List<PlayerInfo> players)
    {
        string path = GetPath(fileName);
        string content = JsonHelper.ToJson<PlayerInfo>(players.ToArray());
        Debug.Log(path);
        WriteFile(path, content); 
    }

    public List<PlayerInfo> LoadPlayerList()
    {
        string path = GetPath(fileName);
        Debug.Log(path);
        string content = ReadFile(path);

        if (string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<PlayerInfo>();
        }

        List<PlayerInfo> res = JsonHelper.FromJson<PlayerInfo>(content).ToList();

        return res;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }
        return "";
    }

    private static string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
