using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MatchingLevelEditor : LevelEditor<MatchingLevelData>
{

    public Transform pairsContainer;
    public GameObject pairRowPrefab;

    public Button addPairButton;

    public int MAX_PAIRS = 6;

    private List<PairRow> pairRows = new List<PairRow>();

    protected new void Start()
    {
        base.Start();
        addPairButton.onClick.AddListener(AddPairRow);

        inputTitle.text = levelData?.title ?? "";
        difficultyDropdown.value = (levelData != null) ? levelData.difficulty - 1 : 0;
        foreach (string[] pair in levelData.pairs)
        {
            PairRow row = InstantiateNewPairRow();
            row.SetLeft(GetSpriteByName(pair[0]));
            row.SetRight(GetSpriteByName(pair[1]));
            pairRows.Add(row);
        }

        if(pairRows.Count >= MAX_PAIRS)
        {
            addPairButton.interactable = false;
        }
    }

    Sprite GetSpriteByName(string name)
    {
        var sprites = SpriteLoader.LoadAllSprites();

        return sprites.FirstOrDefault(s => s.texture.name == name);
    }

    void AddPairRow()
    {
        if(pairRows.Count < MAX_PAIRS)
        {
            pairRows.Add(InstantiateNewPairRow());

            if(pairRows.Count >= MAX_PAIRS)
            {
                addPairButton.interactable = false;
            }
        }
    }

    PairRow InstantiateNewPairRow() {
        GameObject rowObj = Instantiate(pairRowPrefab, pairsContainer);
        PairRow row = rowObj.GetComponent<PairRow>();

        row.leftButton.onClick.AddListener(() => SelectImageFor(row, true));
        row.rightButton.onClick.AddListener(() => SelectImageFor(row, false));
        return row;
    }

    public void SelectImageFor(PairRow row, bool isLeft)
    {
        SelectImage((Sprite sprite) =>
        {
            if (isLeft)
                row.SetLeft(sprite);
            else
                row.SetRight(sprite);
        });
    }

    protected override void PersistLevel()
    {
        List<string[]> pairs = new List<string[]>();

        foreach (PairRow row in pairRows)
        {
            if (string.IsNullOrEmpty(row.leftImage) || string.IsNullOrEmpty(row.rightImage))
                continue;

            pairs.Add(new string[] { row.leftImage, row.rightImage });
        }

        MatchingLevelData level = new MatchingLevelData
        {
            id = LevelLoader.CurrentLevel.id,
            title = inputTitle.text,
            difficulty = difficultyDropdown.value + 1,
            scene = "MatchingScene",
            pairs = pairs.ToArray()
        };

        LevelLoader.SaveLevel(level);
    }
}
