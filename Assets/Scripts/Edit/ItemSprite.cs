using UnityEngine;
using UnityEngine.UI;

public class ItemSprite : MonoBehaviour
{
    public Text label;
    public Button button;

    [HideInInspector] 
    public string image;

    public void SetSprite(Sprite sprite)
    {
        if(sprite == null) return;
        image = sprite.texture.name;
        label.text = sprite.texture.name;
        button.GetComponent<Image>().sprite = sprite;
    }

}
