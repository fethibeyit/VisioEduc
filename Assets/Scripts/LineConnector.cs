using UnityEngine;

public class LineConnector : MonoBehaviour
{
    public static LineConnector Instance;

    public GameObject linePrefab;

    private LineRenderer currentLine;
    private ConnectableItem startItem;

    public MatchingLevelBuilder builder;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (currentLine != null && currentLine.gameObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            currentLine.SetPosition(1, mousePos);

            if (Input.GetMouseButtonUp(0))
                EndLine(null);
        }
        else
        {
            currentLine = null;
        }
    }

    public void StartLine(ConnectableItem item)
    {
        startItem = item;
        builder = item.builder;

        GameObject lineObj = Instantiate(linePrefab);
        currentLine = lineObj.GetComponent<LineRenderer>();

        Vector3 pos = item.transform.position;
        currentLine.positionCount = 2;
        currentLine.SetPosition(0, pos);
        currentLine.SetPosition(1, pos);
    }

    public void EndLine(ConnectableItem _)
    {
        if (currentLine == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        ConnectableItem endItem = null;
        if (hit.collider != null)
            endItem = hit.collider.GetComponent<ConnectableItem>();

        if (startItem != null && endItem != null && startItem != endItem && endItem.id == startItem.id)
        {
            // Vérifie que les deux images ne sont pas déjà connectées
            if (!startItem.isConnected && !endItem.isConnected)
            {
                currentLine.SetPosition(1, endItem.transform.position);

                // Marque les deux images comme connectées
                startItem.isConnected = true;
                endItem.isConnected = true;
                builder.RegisterPlacement();
            }
            else
            {
                Debug.Log("Cette paire est déjà connectée !");
                Destroy(currentLine.gameObject);
            }

            currentLine = null;
            startItem = null;
            return;
        }

        // Ligne invalide
        Destroy(currentLine.gameObject);
        currentLine = null;
        startItem = null;
    }
}