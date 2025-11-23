using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public string title;
    public int difficulty;
    public string scene;
}

[System.Serializable]
public class MatchingLevelData : LevelData
{
    public string[][] pairs;
}

[System.Serializable]
public class MatchingLevelsRoot
{
    public MatchingLevelData[] levels;
}

public static class MatchingLevelLoader
{
    public static MatchingLevelsRoot LoadLevels()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/MatchingLevels");

        if (jsonFile == null)
        {
            Debug.LogError("DATA JSON introuvable !");
            return null;
        }

        return JsonConvert.DeserializeObject<MatchingLevelsRoot>(jsonFile.text);
    }
}

[System.Serializable]
public class SortingLevelsRoot
{
    public SortingLevelData[] levels;
}

[System.Serializable]
public class SortingLevelData : LevelData
{
    public Dictionary<string, string[]> categories;
}

public static class SortingLevelLoader
{
    public static SortingLevelsRoot LoadLevels()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Data/SortingLevels");

        if (jsonFile == null)
        {
            Debug.LogError("DATA JSON introuvable !");
            return null;
        }

        return JsonConvert.DeserializeObject<SortingLevelsRoot>(jsonFile.text);
    }
}