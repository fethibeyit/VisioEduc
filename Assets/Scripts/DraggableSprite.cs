using System.Collections;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    public string category;

    private Vector3 offset;
    private bool dragging = false;
    private Vector3 originalScale;

    private SpriteRenderer sr;
    private int originalSortingOrder;


    private void Start()
    {
        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        originalSortingOrder = sr.sortingOrder;

    }

    void OnMouseDown()
    {
        dragging = true;
        SoundManager.Instance.PlayClick();
        transform.localScale = originalScale * 1.3f;

        sr.sortingOrder = 1000;
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
        transform.localScale = originalScale;

        SortingLevelBuilder.Instance.OnItemReleased(this);

        StartCoroutine(RestoreSortingOrderAfterDelay(0.5f));

    }

    private IEnumerator RestoreSortingOrderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sr.sortingOrder = originalSortingOrder;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 m = Input.mousePosition;
        m.z = 10f; // distance de la caméra
        return Camera.main.ScreenToWorldPoint(m);
    }
}
