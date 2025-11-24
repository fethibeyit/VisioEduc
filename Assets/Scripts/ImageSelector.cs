using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageSelector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform content;
    public Action<Sprite> onSelectImage;

    private Sprite[] sprites;

    void Start()
    {
        LoadImages();
    }

    void LoadImages()
    {
        sprites = SpriteLoader.LoadAllSprites();

        Debug.Log($"Loaded {sprites.Length} sprites.");

        foreach (var sprite in sprites)
        {
            GameObject btn = Instantiate(buttonPrefab, content);
            btn.GetComponent<Image>().sprite = sprite;

            string imgName = sprite.name;

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                onSelectImage?.Invoke(sprite);
                gameObject.SetActive(false);
            });
        }
    }
}

public static class SpriteLoader
{
    public static Sprite[] LoadAllSprites()
    {
        return Resources.LoadAll<Sprite>("Images");
    }
}