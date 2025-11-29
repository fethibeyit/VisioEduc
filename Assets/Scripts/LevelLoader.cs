using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class LevelLoader
{
    public static LevelData CurrentLevel;

    public static readonly Type[] LevelTypes = new Type[]
    {
        typeof(MatchingLevelData),
        typeof(SortingLevelData)
    };

    public static List<LevelData> AllLevels =>
            LevelsDict.Values.SelectMany(list => list).ToList();

    public static Dictionary<Type, List<LevelData>> LevelsDict = new();

    public static LevelsRoot<T> LoadLevels<T>() where T : LevelData
    {
        string fileName = $"{typeof(T).Name}.json";   

        string jsonText = JsonHelper.LoadJson(fileName);
        return JsonConvert.DeserializeObject<LevelsRoot<T>>(jsonText);
    }

    public static void LoadAllLevels()
    {
        LevelsDict.Clear();

        foreach (var type in LevelTypes)
        {
            var method = typeof(LevelLoader)
                .GetMethod(nameof(LoadLevels), BindingFlags.Static | BindingFlags.Public)
                .MakeGenericMethod(type);

            var result = method.Invoke(null, null);

            var levelsField = result.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance)[0];
            var levelsArray = (Array)levelsField.GetValue(result);
            var levelsList = levelsArray.Cast<LevelData>().ToList();
            LevelsDict[type] = levelsList;
        }
    }

    public static void SaveLevel<T>(T level) where T : LevelData 
    { 
        var idx = -1;
        if (level.id != Guid.Empty)
        {
            idx = LevelsDict[typeof(T)].FindIndex(l => l.id == level.id);
        }

        if (idx >= 0)
        {
            LevelsDict[typeof(T)][idx] = level;
        }
        else
        {
            level.id = Guid.NewGuid();
            LevelsDict[typeof(T)].Add(level);
        }

        var levelsRoot = new LevelsRoot<T>
        {
            levels = LevelsDict[typeof(T)].Cast<T>().ToArray()
        };

        string fileName = $"{typeof(T).Name}.json";
        var jsonText = JsonConvert.SerializeObject(levelsRoot, Formatting.Indented);

        JsonHelper.SaveJson(jsonText, fileName);
    }

}

