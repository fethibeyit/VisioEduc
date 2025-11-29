using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<LevelData> currentLevels = new();

    private LevelData currentLevelData;

    public int currentLevelIndex = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        DataInitializer.CopyDataFilesToPersistent();
        LevelLoader.LoadAllLevels();
    }

    public void LoadLevels(int difficulty = 1)
    {
        currentLevels = LevelLoader.AllLevels.Where(x => x.difficulty == difficulty).ToList();
        currentLevels.Shuffle();

        LaunchCurrentLevel();
    }

    public void LoadEditLevel()
    {
        SceneManager.LoadScene("LevelSelectorScene");
    }

    public void LaunchCurrentLevel()
    {
        if (currentLevelIndex >= currentLevels.Count)
        {
            Debug.Log("Tous les niveaux sont terminés !");

            SceneManager.LoadScene("PlayVideo");
            return;
        }

        currentLevelData = currentLevels[currentLevelIndex];

        SceneManager.LoadScene(currentLevelData.scene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene chargée : " + scene.name);

        var builder = FindFirstObjectByType<LevelBuilder>();

        if (builder != null)
        {
            builder.BuildLevel(currentLevelData);
        }
    }

    public void CompleteLevel()
    {
        Debug.Log($"Niveau {currentLevelData.title} complété !");
        currentLevelIndex++;
        LaunchCurrentLevel();
    }
}
