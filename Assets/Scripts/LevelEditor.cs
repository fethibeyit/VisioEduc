using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class LevelEditor<T> : MonoBehaviour
    where T : LevelData
{
    protected T levelData;

    public InputField inputTitle;
    public Dropdown difficultyDropdown;

    public ImageSelector imageSelector;
    private Action<Sprite> onSelected;

    public Button saveButton;
    public Button cancelButton;


    protected void Start()
    {
        levelData = StaticLevelEdit.CurrentLevel as T;
        saveButton.onClick.AddListener(Save);
        cancelButton.onClick.AddListener(Cancel);
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

    protected void SelectImage(Action<Sprite> callback)
    {
        onSelected = callback;

        imageSelector.onSelectImage = (Sprite sprite) =>
        {
            onSelected?.Invoke(sprite);
        };

        imageSelector.gameObject.SetActive(true);
    }
}

