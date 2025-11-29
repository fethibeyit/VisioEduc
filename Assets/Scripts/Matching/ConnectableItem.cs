using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConnectableItem : MonoBehaviour
{
    [Header("ID de la paire")]
    public int id;

    public bool isConnected = false;

    public MatchingLevelBuilder builder;

    void OnMouseDown()
    {
        if (isConnected)
        {
            Debug.Log($"{gameObject.name} est déjà connecté !");
            return;
        }
        SoundManager.Instance.PlayClick();
        LineConnector.Instance?.StartLine(this);
    }
}