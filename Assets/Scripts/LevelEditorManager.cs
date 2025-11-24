using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorManager : MonoBehaviour
{
    public InputField inputTitle;
    public Dropdown difficultyDropdown;

    public Transform pairsContainer;
    public GameObject pairRowPrefab;

    public ImageSelector imageSelector;

    private PairRow selectedPairRow;
    private bool selectingLeft;

    public Button addPairButton;
    public Button saveButton;

    private void Start()
    {
        addPairButton.onClick.AddListener(AddPairRow);
        saveButton.onClick.AddListener(SaveLevel);
    }

    void AddPairRow()
    {
        GameObject rowObj = Instantiate(pairRowPrefab, pairsContainer);
        PairRow row = rowObj.GetComponent<PairRow>();

        row.leftButton.onClick.AddListener(() => SelectImageFor(row, true));
        row.rightButton.onClick.AddListener(() => SelectImageFor(row, false));
    }

    void SelectImageFor(PairRow row, bool isLeft)
    {
        selectedPairRow = row;
        selectingLeft = isLeft;

        imageSelector.onSelectImage = (Sprite sprite) =>
        {
            if (selectingLeft)
                row.SetLeft(sprite);
            else
                row.SetRight(sprite);
        };

        imageSelector.gameObject.SetActive(true);
    }

    void SaveLevel()
    {
        List<string[]> pairs = new List<string[]>();

        foreach (Transform t in pairsContainer)
        {
            PairRow row = t.GetComponent<PairRow>();
            if (string.IsNullOrEmpty(row.leftImage) || string.IsNullOrEmpty(row.rightImage))
                continue;

            pairs.Add(new string[] { row.leftImage, row.rightImage });
        }

        var matchingLevelsRoot = MatchingLevelLoader.LoadLevels();


        MatchingLevelData level = new MatchingLevelData
        {
            title = inputTitle.text,
            difficulty = difficultyDropdown.value,
            scene = "MatchingScene",
            pairs = pairs.ToArray()
        };

        matchingLevelsRoot.levels = matchingLevelsRoot.levels.Append(level).ToArray();

        string jsonText = JsonConvert.SerializeObject(matchingLevelsRoot, Formatting.Indented);

        JsonHelper.SaveJson("MatchingLevels.json", jsonText);
    }
}
