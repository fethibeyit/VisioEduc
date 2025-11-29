using System;
using System.Collections.Generic;

public class LevelData
{
    public Guid id;
    public string title;
    public int difficulty;
    public string scene;
}

[Serializable]
public class LevelsRoot<T> where T : LevelData
{
    public T[] levels;
}

[Serializable]
public class MatchingLevelData : LevelData
{
    public string[][] pairs;
}

[Serializable]
public class SortingLevelData : LevelData
{
    public Dictionary<string, string[]> categories;
}





