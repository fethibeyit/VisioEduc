using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortingLevelBuilder : LevelBuilder
{
    public static SortingLevelBuilder Instance;

    private Camera cam;

    [Header("Spawner")]
    public Transform spawnPoint;
    public GameObject spritePrefab;

    [Header("Grids")]
    public GridPlacer leftGrid;
    public GridPlacer rightGrid;

    [Header("Drop Zones")]
    public DropZone2D leftZone;
    public DropZone2D rightZone;

    private List<(Sprite, string)> items;
    private int index = 0;

    void Awake()
    {
        Instance = this;
    }

    public override void BuildLevel(LevelData levelData)
    {
        cam = Camera.main;

        ClearLevel();

        var data = levelData as SortingLevelData;

        Debug.Log(data.title);

        

        items = new List<(Sprite, string)>();

        //var levels = SortingLevelLoader.LoadLevels();

        Debug.Log("Loaded level: " + data.title);

        foreach (string category in data.categories.Keys)
        {
            foreach (string sprite in data.categories[category])
            {
                items.Add((Resources.Load<Sprite>("Images/" + sprite), category));
            }
        }

        totalPairs = items.Count;
        pairsPlaced = 0;

        var leftCategory = data.categories.Keys.ElementAt(0);
        var rightCategory = data.categories.Keys.ElementAt(1);

        leftGrid.category = leftCategory;
        rightGrid.category = rightCategory;

        leftZone.acceptedCategory = leftCategory;
        leftZone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + leftCategory);

        rightZone.acceptedCategory = rightCategory;
        rightZone.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + rightCategory);

        Shuffle(items);
        SpawnNext();
    }

    public void OnItemReleased(DraggableSprite item)
    {
        Vector2 p = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(p, Vector2.zero);

        DropZone2D zone = null;
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<DropZone2D>(out zone))
                break;
        }

        if (zone != null && zone.acceptedCategory == item.category)
        {
            item.transform.localScale *= 0.5f;

            GridPlacer grid = (item.category == leftGrid.category) ? leftGrid : rightGrid;

            item.transform.position = grid.GetNextPosition();
            
            RegisterPlacement();

            SpawnNext();
        }
        else
        {
            item.transform.position = spawnPoint.position;
        }
    }

    void SpawnNext()
    {
        if (index >= items.Count)
        {
            Debug.Log("TERMINÉ !");
            return;
        }

        var (spr, cat) = items[index++];
        GameObject go = Instantiate(spritePrefab, spawnPoint.position, Quaternion.identity);
        go.GetComponent<SpriteRenderer>().sprite = spr;
        go.GetComponent<DraggableSprite>().category = cat;
        spawnedObjects.Add(go);
    }
}
