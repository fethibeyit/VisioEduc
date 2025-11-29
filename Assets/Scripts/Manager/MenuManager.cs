using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    public TMP_Dropdown difficultyDropdown;

    public Button startButton;
    public Button editLevelButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        editLevelButton.onClick.AddListener(EditLevel);
    }

    void StartGame()
    {
        GameManager.Instance.LoadLevels(difficultyDropdown.value + 1);
    }

    void EditLevel()
    {
       GameManager.Instance.LoadEditLevel();
    }

}
