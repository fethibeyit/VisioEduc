using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class LevelEditor<T> : SelectSprite
    where T : LevelData
{
    protected T levelData;

    public InputField inputTitle;
    public Dropdown difficultyDropdown;


    public Button saveButton;
    public Button cancelButton;


    protected void Start()
    {
        levelData = LevelLoader.CurrentLevel as T;
        saveButton.onClick.AddListener(Save);
        cancelButton.onClick.AddListener(Cancel);
        inputTitle.text = levelData?.title ?? "";
        difficultyDropdown.value = (levelData != null) ? levelData.difficulty - 1 : 0;
    }

    protected abstract void PersistLevel();
    protected void Save()
    {
        PersistLevel();
        Cancel();
    }

    protected void Cancel()
    {
        SceneManager.LoadScene("LevelSelectorScene");
    }

}

