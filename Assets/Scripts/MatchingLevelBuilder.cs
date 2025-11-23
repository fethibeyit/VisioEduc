using System.Collections.Generic;
using UnityEngine;

public class MatchingLevelBuilder : LevelBuilder
{
    public GameObject itemPrefab;
    public GameObject linePrefab;

    public Transform leftColumn;
    public Transform rightColumn;

    public override void BuildLevel(LevelData levelData)
    {
        ClearLevel();

        var data = levelData as MatchingLevelData;

        Debug.Log(data.title);

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

            var leftConnectable = item.GetComponent<ConnectableItem>();
            leftConnectable.id = i+1;
            leftConnectable.builder = this;

            spawnedObjects.Add(item);

            int pos = shuffled[i];  

            GameObject zone = Instantiate(itemPrefab, rightColumn);
            zone.transform.localPosition = new Vector3(0, -pos * 2f, 0);  

            zone.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Images/" + pair[1]);

            var rightConnectable = zone.GetComponent<ConnectableItem>();
            rightConnectable.id = i+1;
            rightConnectable.builder = this;

            spawnedObjects.Add(zone);
        }
    }

}
