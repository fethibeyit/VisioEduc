using System.IO;
using UnityEngine;

public static class JsonHelper{
    public static string LoadJson(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, "Data", fileName);

        if (!File.Exists(path))
        {
            Debug.LogError("JSON file not found: " + path);
            return null;
        }

        return File.ReadAllText(path);
    }

    public static void SaveJson(string fileName, string json)
    {
        string folder = Path.Combine(Application.persistentDataPath, "Data");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, fileName);

        File.WriteAllText(path, json);

        Debug.Log("Saved JSON to : " + path);
    }
}
