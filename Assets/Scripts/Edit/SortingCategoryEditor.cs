using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SortingCategoryEditor : SelectSprite
{
    public Transform ItemsContainer;
    public GameObject ItemSpritePrefab;
    public Button addItemButton;

    public int MAX_ITEMS = 9;

    public ItemSprite categorySprite;
    private List<ItemSprite> items = new();

    protected void Start()
    {
        addItemButton.onClick.AddListener(AddItem);
        categorySprite.button.onClick.AddListener(() => SelectImageFor(categorySprite));
    }

    public void LoadItems(string category, string[] images)
    {
        categorySprite.SetSprite(GetSpriteByName(category));

        foreach (string image in images)
        {
            ItemSprite item = InstantiateNewItemSprite();
            item.SetSprite(GetSpriteByName(image));
            items.Add(item);
        }

        if (items.Count >= MAX_ITEMS)
        {
            addItemButton.interactable = false;
        }
    }

    public Dictionary<string, string[]> GetCategory()
    {
        Dictionary<string, string[]> data = new();
        string categoryName = categorySprite.image;
        string[] itemNames = items
            .Where(i => !string.IsNullOrEmpty(i.image))
            .Select(i => i.image)
            .ToArray();
        data[categoryName] = itemNames;
        return data;
    }

    Sprite GetSpriteByName(string name)
    {
        var sprites = SpriteLoader.LoadAllSprites();

        return sprites.FirstOrDefault(s => s.texture.name == name);
    }

    void AddItem()
    {
        if (items.Count < MAX_ITEMS)
        {
            items.Add(InstantiateNewItemSprite());

            if (items.Count >= MAX_ITEMS)
            {
                addItemButton.interactable = false;
            }
        }
    }

    ItemSprite InstantiateNewItemSprite()
    {
        GameObject itemObj = Instantiate(ItemSpritePrefab, ItemsContainer);
        ItemSprite item = itemObj.GetComponent<ItemSprite>();

        item.button.onClick.AddListener(() => SelectImageFor(item));
        return item;
    }

    public void SelectImageFor(ItemSprite item)
    {
        SelectImage((Sprite sprite) => item.SetSprite(sprite));
    }
}
