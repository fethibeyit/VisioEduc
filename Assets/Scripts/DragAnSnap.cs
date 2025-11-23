using UnityEngine;

public class DragAndSnap : MonoBehaviour
{
    [Header("ID du sprite (ex: A1)")]
    public string itemID;

    private Camera cam;
    private Vector3 originalPosition;
    private Collider2D selfCollider;
    private bool isDragging = false;

    private bool isLocked = false;

    public DragAndSnapLevelBuilder builder;

    void Start()
    {
        cam = Camera.main;
        originalPosition = transform.position;
        selfCollider = GetComponent<Collider2D>();

        if (selfCollider == null)
            Debug.LogError("Aucun Collider2D sur : " + gameObject.name);
    }

    void OnMouseDown()
    {
        if (isLocked) return;
        isDragging = true;
    }

    void OnMouseUp()
    {
        if (isLocked) return;

        isDragging = false;
        CheckPossiblePlacement();
    }

    void Update()
    {
        if (!isDragging || isLocked)
            return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }

    void CheckPossiblePlacement()
    {
        Vector2 p = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(p, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.collider == null)
                continue;

            if (hit.collider == selfCollider)
                continue;

            PlacementZone zone = hit.collider.GetComponent<PlacementZone>();
            if (zone != null)
            {
                if (zone.zoneID == itemID)
                {
                    transform.position = zone.transform.position;
                    isLocked = true;

                    builder.RegisterPlacement();
                    return;
                }
                else
                {
                    ResetPosition();
                    return;
                }
            }
        }

        // ❌ Rien touché → retour
        ResetPosition();
    }

    void ResetPosition()
    {
        transform.position = originalPosition;
    }
}