using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    public string category;

    private Vector3 offset;
    private bool dragging = false;

    void OnMouseDown()
    {
        dragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        if (dragging)
            transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        dragging = false;

        Debug.Log("Item released: " + gameObject);

        // Vérification dans quelle zone on est tombé
        SortingLevelBuilder.Instance.OnItemReleased(this);
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 m = Input.mousePosition;
        m.z = 10f; // distance de la caméra
        return Camera.main.ScreenToWorldPoint(m);
    }
}
