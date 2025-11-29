using System.IO;
using UnityEngine;
public static class DataInitializer
{
    public static void CopyDataFilesToPersistent()
    {
        string targetFolder = Path.Combine(Application.persistentDataPath, "Data");

        if (!Directory.Exists(targetFolder))
            Directory.CreateDirectory(targetFolder);

        // Charger tous les text assets du dossier Resources/Data
        TextAsset[] files = Resources.LoadAll<TextAsset>("Data");

        foreach (var file in files)
        {
            string filePath = Path.Combine(targetFolder, file.name + ".json");

            // Copier UNIQUEMENT si le fichier n'existe pas déjà
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, file.text);
                Debug.Log("Copied default file: " + filePath);
            }
        }
    }
}
