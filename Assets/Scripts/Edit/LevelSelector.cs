using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject levelButtonPrefab;

    public Button addMatchingButton;
    public Button addSortingButton;

    public Button returnButton;


    void Start()
    {
        addMatchingButton.onClick.AddListener(() =>
        {
            OpenLevelEditor(new LevelData { scene = "MatchingScene"});
        });

        addSortingButton.onClick.AddListener(() =>
        {
            OpenLevelEditor(new LevelData { scene = "SortingScene" });
        });

        returnButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BootScene");
        });

        PopulateUI();
    }

    void PopulateUI()
    {
        foreach (LevelData lvl in LevelLoader.AllLevels)
        {
            GameObject button = Instantiate(levelButtonPrefab, contentPanel);

            button.GetComponentInChildren<TMP_Text>().text =
                $"{lvl.title}  (Difficulté: {lvl.difficulty}) [{lvl.id}]";

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenLevelEditor(lvl);
            });
        }
    }

    void OpenLevelEditor(LevelData lvl)
    {
        Debug.Log("Ouverture du niveau : " + lvl.title);

        LevelLoader.CurrentLevel = lvl;

        SceneManager.LoadScene($"Edit{lvl.scene}");
    }


}