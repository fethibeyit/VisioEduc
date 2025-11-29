using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject levelButtonPrefab;

    public Button addButton;


    void Start()
    {
        addButton.onClick.AddListener(() =>
        {
            OpenLevelEditor(new LevelData { scene = "MatchingScene"});
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