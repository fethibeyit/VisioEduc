using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public string category;
    public int columns = 3;
    public float cellSize = 1.2f;

    private int count = 0;

    public Vector3 GetNextPosition()
    {
        int row = count / columns;
        int col = count % columns;

        Vector3 pos = transform.position + new Vector3(col * cellSize, -row * cellSize, 0);

        count++;
        return pos;
    }
}
