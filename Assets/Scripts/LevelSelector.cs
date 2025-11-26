using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject levelButtonPrefab;

    private List<LevelData> levelList = new();

    void Start()
    {
        LoadLevels();
        PopulateUI();
    }

    void LoadLevels()
    {

        levelList.AddRange(MatchingLevelLoader.LoadLevels().levels);
        levelList.AddRange(SortingLevelLoader.LoadLevels().levels);


        if (levelList.Count == 0)
            Debug.LogWarning("Empty Level List");
    }

    void PopulateUI()
    {
        foreach (LevelData lvl in levelList)
        {
            GameObject button = Instantiate(levelButtonPrefab, contentPanel);

            button.GetComponentInChildren<TMP_Text>().text =
                $"{lvl.title}  (Difficulté: {lvl.difficulty})";

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                OpenLevelEditor(lvl);
            });
        }
    }

    void OpenLevelEditor(LevelData lvl)
    {
        Debug.Log("Ouverture du niveau : " + lvl.title);

        StaticLevelEdit.CurrentLevel = lvl;

        SceneManager.LoadScene($"Edit{lvl.scene}");

        // On peut transférer le niveau choisi vers l’éditeur
        //LevelEditor.LoadedLevel = lvl;

        //UnityEngine.SceneManagement.SceneManager.LoadScene(lvl.scene);
    }
}