using System;
using UnityEngine;

public class SelectSprite : MonoBehaviour
{
    public ImageSelector imageSelector;
    private Action<Sprite> onSelected;

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

