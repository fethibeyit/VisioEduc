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

    public int currentDifficulty = 1;

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

        // read levels from Json Data
        currentLevels.AddRange(MatchingLevelLoader.LoadLevels().levels.Where(x=>x.difficulty == currentDifficulty));
        currentLevels.AddRange(SortingLevelLoader.LoadLevels().levels.Where(x => x.difficulty == currentDifficulty));
 
        currentLevels.Shuffle();

        LoadLevel();
    }

    public void LoadLevel()
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
        Debug.Log("Scene chargée : " + scene.name + scene.buildIndex);

        if (scene.name == "BootScene" || scene.name == "PlayVideo") return;

        var builder = FindFirstObjectByType<LevelBuilder>();

        if (builder == null)
        {
            Debug.LogError("LevelBuilder introuvable dans la scène!");
            return;
        }

        builder.BuildLevel(currentLevelData);

        Debug.Log($"Niveau {currentLevelData.title} de difficulté {currentLevelData.difficulty} instancié!");
    }

    public void CompleteLevel()
    {
        Debug.Log($"Niveau {currentLevelData.title} complété !");
        currentLevelIndex++;
        LoadLevel();
    }
}
