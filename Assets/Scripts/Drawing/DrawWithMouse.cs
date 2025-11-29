using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 previousPosition;

    [SerializeField]
    private float minDistance = 0.1f; // Minimum distance to register a new point

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0; // Assuming a 2D plane at z=0

            if(Vector3.Distance(currentPosition, previousPosition) > minDistance)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
                previousPosition = currentPosition;
            }
        }
    }
}
 