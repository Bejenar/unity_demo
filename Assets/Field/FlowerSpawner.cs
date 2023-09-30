using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public int i;

    public int j;

    public Flower flower;

    private GridManager _gridManager;

    private void Start()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }

    public void Plant()
    {
        Debug.LogFormat("Attempting planting {2} on [{0}-{1}]", i,j, flower.flowerName);
        _gridManager.GetCell(i,j).OnFlowerAdded(flower);
    }
}
