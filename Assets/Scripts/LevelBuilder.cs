using System.Collections.Generic;
using UnityEngine;

public abstract class LevelBuilder : MonoBehaviour
{
    protected int pairsPlaced = 0;
    protected int totalPairs;

    protected List<GameObject> spawnedObjects = new List<GameObject>();

    public abstract void BuildLevel(LevelData data);

    protected void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }

    public void RegisterPlacement()
    {
        pairsPlaced++;

        if (pairsPlaced >= totalPairs)
        {
            Debug.Log("Tous les objets sont placés !");
            GameManager.Instance.CompleteLevel();
        }
    }

    protected void ClearLevel()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }

        spawnedObjects.Clear();
    }
}
