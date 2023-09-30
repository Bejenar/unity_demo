using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public Flower currentFlower = null;

    public List<Cell> _neighbours = new();

    public string code;

    private Image _flowerImage;

    private GridManager _gridManager;

    private ScoreManager _scoreManager;

    // Start is called before the first frame update
    void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        var component = transform.Find("Flower").GetComponent<Image>();
        _flowerImage = component;
        _scoreManager = FindObjectOfType<ScoreManager>();
        RemoveFlower();
    }

    private void RemoveFlower()
    {
        var flowerImageColor = _flowerImage.color;
        flowerImageColor.a = 0;
        _flowerImage.color = flowerImageColor;
        currentFlower = null;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddNeighbour(Cell neighbour)
    {
        _neighbours.Add(neighbour);
    }

    public void OnFlowerAdded(Flower flower)
    {
        if (currentFlower)
        {
            Debug.Log("there is already a flower on this cell");
            return;
        }


        var neighboursWithFlowers = NeighboursWithFlowers();

        if (IsAnyNeighbourIncompatible(neighboursWithFlowers, flower))
        {
            Debug.Log("Can not plant here. Incompatible plant nearby");
            currentFlower = null;
            return;
        }

        bool flowersOfSameTypePresent = _gridManager.FindCellsWithFlower(flower).Count > 0;
        if (flowersOfSameTypePresent && IsNoSameNeighbours(neighboursWithFlowers, flower))
        {
            Debug.Log("Can not plant here. No neighbour with the same color");
            currentFlower = null;
            return;
        }

        currentFlower = flower;
        _flowerImage.color = flower.color;
        var flowerImageColor = _flowerImage.color;
        flowerImageColor.a = 255;
        _flowerImage.color = flowerImageColor;

        if (neighboursWithFlowers.Count == 0)
        {
            Debug.Log("Added flower no problem as neighbor cells are empty");
            return;
        }


        var chain = new HashSet<Cell>();
        neighboursWithFlowers.ForEach(cell => Debug.Log(cell.code));
        GetWholeNeighbourChain(neighboursWithFlowers, chain);

        Debug.Log("Chain size is " + chain.Count);
        if (IsChainSameFlower(chain))
        {
            return;
        }

        if (chain.Count < 3)
        {
            Debug.Log("too small chain");
            return;
        }

        var uniqueFlowers = FindUniqueFlowersInChain(chain);
        // detected reaction
        foreach (var cell in chain)
        {
            cell.RemoveFlower();
        }

        _gridManager.PlantFlowersAtRandomSpot(uniqueFlowers);
        _scoreManager.AddScore(Math.Max(chain.Count - 3, 0) * 50 + 500);
    }

    public List<Cell> NeighboursWithFlowers()
    {
        List<Cell> withFlowers = new();
        foreach (var neighbour in _neighbours)
        {
            if (neighbour.currentFlower != null) withFlowers.Add(neighbour);
        }

        return withFlowers;
    }

    private bool IsAnyNeighbourIncompatible(List<Cell> neighbours, Flower flower)
    {
        return neighbours.Any(cell => !cell.currentFlower.IsCompatible(flower));
    }

    private bool IsNoSameNeighbours(List<Cell> neighbours, Flower flower)
    {
        return neighbours.All(cell => !cell.currentFlower.IsSameFlower(flower));
    }

    private void GetWholeNeighbourChain(List<Cell> neighbours, HashSet<Cell> visited)
    {
        if (visited.Contains(this))
        {
            return;
        }

        visited.Add(this);

        foreach (var neighbour in neighbours)
        {
            neighbour.GetWholeNeighbourChain(neighbour.NeighboursWithFlowers(), visited);
        }
    }

    private bool IsChainSameFlower(HashSet<Cell> chain)
    {
        foreach (var cell in chain)
        {
            if (currentFlower.flowerName != cell.currentFlower.flowerName) return false;
        }

        return true;
    }

    private HashSet<Flower> FindUniqueFlowersInChain(HashSet<Cell> chain)
    {
        var uniqueFlowers = new HashSet<Flower>();

        foreach (var cell in chain)
        {
            uniqueFlowers.Add(cell.currentFlower);
        }

        return uniqueFlowers;
    }

    public override string ToString()
    {
        return code;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked at " + code + " Data " + eventData);
    }
}