using Newtonsoft.Json;
using System.Collections.Generic;

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
        var jsonText = JsonHelper.LoadJson("MatchingLevels.json");
        return JsonConvert.DeserializeObject<MatchingLevelsRoot>(jsonText);
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
        var jsonText = JsonHelper.LoadJson("SortingLevels.json");
        return JsonConvert.DeserializeObject<SortingLevelsRoot>(jsonText);
    }
}