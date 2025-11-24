using UnityEngine;
using UnityEngine.UI;

public class PairRow : MonoBehaviour
{
    public Text leftLabel;
    public Text rightLabel;

    public Button leftButton;
    public Button rightButton;

    [HideInInspector] public string leftImage;
    [HideInInspector] public string rightImage;

    public void SetLeft(Sprite sprite)
    {
        leftImage = sprite.texture.name;
        leftLabel.text = sprite.texture.name;
        leftButton.GetComponent<Image>().sprite = sprite;
    }

    public void SetRight(Sprite sprite)
    {
        rightImage = sprite.texture.name;
        rightLabel.text = sprite.texture.name;
        rightButton.GetComponent<Image>().sprite = sprite;
    }
}
