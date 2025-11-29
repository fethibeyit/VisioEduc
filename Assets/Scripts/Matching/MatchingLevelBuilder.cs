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

        // ----------------------
        // Calcul automatique de l'espacement selon l'écran
        // ----------------------
        float camHeight = 2f * Camera.main.orthographicSize;
        float verticalPadding = 1f; // tu peux ajuster
        float availableHeight = camHeight - (verticalPadding * 2f);

        float spacing = availableHeight / count;
        float startY = availableHeight / 2f - spacing / 2f; // centré verticalement

        // ----------------------
        // Shuffle de la colonne droite
        // ----------------------
        List<int> shuffled = new List<int>();
        for (int i = 0; i < count; i++) shuffled.Add(i);
        Shuffle(shuffled);

        // ----------------------
        // Instanciation des items
        // ----------------------
        for (int i = 0; i < count; i++)
        {
            string[] pair = data.pairs[i];

            // ----- Côté gauche -----
            GameObject leftItem = Instantiate(itemPrefab, leftColumn);
            leftItem.transform.localPosition =
                new Vector3(0, startY - spacing * i, 0);

            leftItem.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Images/" + pair[0]);

            var leftConnectable = leftItem.GetComponent<ConnectableItem>();
            leftConnectable.id = i + 1;
            leftConnectable.builder = this;

            spawnedObjects.Add(leftItem);

            // ----- Côté droit : position mixée -----
            int pos = shuffled[i];

            GameObject rightItem = Instantiate(itemPrefab, rightColumn);
            rightItem.transform.localPosition =
                new Vector3(0, startY - spacing * pos, 0);

            rightItem.GetComponent<SpriteRenderer>().sprite =
                Resources.Load<Sprite>("Images/" + pair[1]);

            var rightConnectable = rightItem.GetComponent<ConnectableItem>();
            rightConnectable.id = i + 1;
            rightConnectable.builder = this;

            spawnedObjects.Add(rightItem);
        }
    }

    //public override void BuildLevel(LevelData levelData)
    //{
    //    ClearLevel();

    //    var data = levelData as MatchingLevelData;

    //    Debug.Log(data.title);

    //    totalPairs = data.pairs.Length;
    //    pairsPlaced = 0;

    //    int count = data.pairs.Length;

    //    List<int> shuffled = new List<int>();
    //    for (int i = 0; i < count; i++) shuffled.Add(i);

    //    Shuffle(shuffled);  

    //    for (int i = 0; i < count; i++)
    //    {
    //        string[] pair = data.pairs[i];

    //        GameObject item = Instantiate(itemPrefab, leftColumn);
    //        item.transform.localPosition = new Vector3(0, -i * 2f, 0);
    //        item.GetComponent<SpriteRenderer>().sprite =
    //            Resources.Load<Sprite>("Images/" + pair[0]);

    //        var leftConnectable = item.GetComponent<ConnectableItem>();
    //        leftConnectable.id = i+1;
    //        leftConnectable.builder = this;

    //        spawnedObjects.Add(item);

    //        int pos = shuffled[i];  

    //        GameObject zone = Instantiate(itemPrefab, rightColumn);
    //        zone.transform.localPosition = new Vector3(0, -pos * 2f, 0);  

    //        zone.GetComponent<SpriteRenderer>().sprite =
    //            Resources.Load<Sprite>("Images/" + pair[1]);

    //        var rightConnectable = zone.GetComponent<ConnectableItem>();
    //        rightConnectable.id = i+1;
    //        rightConnectable.builder = this;

    //        spawnedObjects.Add(zone);
    //    }
    //}

}
