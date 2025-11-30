using System.Collections.Generic;
using Unity.VisualScripting;

public class SortingLevelEditor : LevelEditor<SortingLevelData>
{
    public SortingCategoryEditor leftCategory;
    public SortingCategoryEditor rightCategory;

    protected new void Start()
    {
        base.Start();
        var i = 0;
        foreach (var key in levelData.categories.Keys)
        {
            if (i == 0)
            {
                leftCategory.LoadItems(key, levelData.categories[key]);
            }
            else if (i == 1)
            {
                rightCategory.LoadItems(key, levelData.categories[key]);
            }
            i++;
        }
    }

    protected override void PersistLevel()
    {
        Dictionary<string, string[]> categories = new();

        categories.AddRange(leftCategory.GetCategory());
        categories.AddRange(rightCategory.GetCategory());

        SortingLevelData level = new SortingLevelData
        {
            id = LevelLoader.CurrentLevel.id,
            title = inputTitle.text,
            difficulty = difficultyDropdown.value + 1,
            scene = "SortingScene",
            categories = categories
        };

        LevelLoader.SaveLevel(level);
    }
}
