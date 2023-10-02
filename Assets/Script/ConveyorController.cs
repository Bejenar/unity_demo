using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    public GameObject[] seedPrefabs;
    public static Flower SelectedFlower;

    [SerializeField] private int queueSize = 3;
    private LinkedList<GameObject> _seedQueue;
    private GridManager _gridManager;
    private void Awake()
    {
        _seedQueue = new();
        _gridManager = FindObjectOfType<GridManager>();
        SpawnInitialSeeds();
        SelectCurrentFlower();
    }

    public void MoveAndSelect()
    {
        Destroy(_seedQueue.First.Value);
        _seedQueue.RemoveFirst();
        AddRandomLast();
        SelectCurrentFlower();
    }

    private void SpawnInitialSeeds()
    {
        for (int i = 0; i < queueSize; i++)
        {
            AddRandomLast();
        }
    }

    private void AddRandomLast()
    {
        int randomIndex = Random.Range(0, Mathf.Clamp(LevelUpManager._level - 1, 2, 5));
        GameObject newSeed = Instantiate(seedPrefabs[randomIndex], transform, false);
        Debug.Log(newSeed + " - " + newSeed.GetInstanceID());
        _seedQueue.AddLast(newSeed);
        Debug.Log("adding seed to the conveyor");
        Debug.Log(_seedQueue.Count);
    }

    private void SelectCurrentFlower()
    {
        Debug.LogFormat("Queue first element is {0}", _seedQueue.First.Value);
        SelectedFlower = _seedQueue.First.Value.GetComponent<Seed>().flower;
        Debug.Log(SelectedFlower.flowerName);
    }
}
