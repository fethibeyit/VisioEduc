using System.Collections.Generic;
using UnityEngine;

public class DragAndSnapLevelBuilder : LevelBuilder
{
    public GameObject itemPrefab;
    public GameObject zonePrefab;

    public Transform leftColumn;
    public Transform rightColumn;

    public override void BuildLevel(LevelData levelData)
    {
        ClearLevel();

        MatchingLevelData data = levelData as MatchingLevelData;

        totalPairs = data.pairs.Length;
        pairsPlaced = 0;

        int count = data.pairs.Length;

        List<int> shuffled = new List<int>();
        for (int i = 0; i < count; i++) shuffled.Add(i);

        Shuffle(shuffled);  

        for (int i = 0; i < count; i++)
        {
            string[] pair = data.pairs[i];

            GameObject item = Instantiate(itemPrefab, leftColumn);
            item.transform.localPosition = new Vector3(0, -i * 2f, 0);
            item.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Images/" + pair[0]);

            var drag = item.GetComponent<DragAndSnap>();
            drag.itemID = $"{i + 1}";
            drag.builder = this;

            spawnedObjects.Add(item);

            int pos = shuffled[i];  

            GameObject zone = Instantiate(zonePrefab, rightColumn);
            zone.transform.localPosition = new Vector3(0, -pos * 2f, 0);  

            zone.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Images/" + pair[1]);

            var z = zone.GetComponent<PlacementZone>();
            z.zoneID = $"{i + 1}";

            spawnedObjects.Add(zone);
        }
    }

}
