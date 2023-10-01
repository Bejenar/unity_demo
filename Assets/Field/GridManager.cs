using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] public int size;
    [SerializeField] private GameObject parent;
    [SerializeField] private float fieldWidth = 852;

    private GridLayoutGroup _grid;
    public Cell[,] _cells;
    
    [SerializeField] private AudioClip audioClip;

    private LevelManager _levelManager;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _grid = parent.GetComponent<GridLayoutGroup>();
        _cells = new Cell[size, size];
        Generate(size);
        ResolveNeighbours();
    }

    void Generate(int size)
    {
        var sideSize = fieldWidth / size;
        _grid.cellSize = new Vector2(sideSize, sideSize);
        _grid.constraintCount = size;

        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
        {
            var cell = Instantiate(prefab, parent.transform, false);
            var component = cell.GetComponent<Cell>();
            component.code = $"({i}-{j})";
            _cells[i, j] = component;
        }
    }
    
    public void PlantFlowersAtRandomSpot(ICollection<Flower> flowersToPlant)
    {
        foreach (var flower in flowersToPlant)
        {
            var availableCells = FindAvailableCells(flower);
            var range = Random.Range(0, availableCells.Count);
            Debug.LogFormat("there are {0} available cells picking cell number {1}", availableCells.Count, range);
            availableCells[range].OnFlowerAdded(flower);
        }
    }

    public List<Cell> FindAvailableCells(Flower flowerToPlant)
    {
        List<Cell> availableCells = new();
        bool flowersOfSameTypePresent = FindCellsWithFlower(flowerToPlant).Count > 0;
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
        {
            var cell = _cells[i, j];
            var curFlower = cell.currentFlower;
            if (curFlower != null)
            {
                continue;
            }
            
            var canPlantHere = !flowersOfSameTypePresent || cell.NeighboursWithFlowers().Any(neighbour => neighbour.currentFlower.IsSameFlower(flowerToPlant)); // ???
            var forbiddenPlant = cell.NeighboursWithFlowers().Any(neighbour => !neighbour.currentFlower.IsCompatible(flowerToPlant));
            if (canPlantHere && !forbiddenPlant)
            {
                availableCells.Add(cell);
            }
        }

        return availableCells;
    }

    public List<Cell> FindCellsWithFlower(Flower flower)
    {
        List<Cell> cellsWithFlower = new List<Cell>();
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
        {
            var cell = _cells[i, j];
            var curFlower = cell.currentFlower;
            if (curFlower != null)
            {
                if (curFlower.IsSameFlower(flower))
                {
                    cellsWithFlower.Add(cell);
                }
            }
        }

        return cellsWithFlower;
    }

    void ResolveNeighbours()
    {
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
        {
            var cell = _cells[i, j];
            if (i != 0) // Add left
            {
                cell.AddNeighbour(_cells[i - 1, j]);
            }

            if (i != size - 1) // Add right
            {
                cell.AddNeighbour(_cells[i + 1, j]);
            }

            if (j != 0) // Add top
            {
                cell.AddNeighbour(_cells[i, j - 1]);
            }

            if (j != size - 1) // Add bottom
            {
                cell.AddNeighbour(_cells[i, j + 1]);
            }
        }
    }

    public Cell GetCell(int i, int j)
    {
        return _cells[i, j];
    }

    public void DestroyCells()
    {
        foreach (var cell in _cells)
        {
            Destroy(cell.gameObject);
        }
    }

    public void SpawnObstacles(int count)
    {
        
    }

    public void CheckIfNoAvailableTurns()
    {
        var availableCells = FindAvailableCells(ConveyorController.SelectedFlower);
        if (availableCells.Count == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.LogError("game over");
        AudioSource.PlayClipAtPoint(audioClip, Vector2.zero);
        isGameOver = true;
        LevelUpManager._level = 3;
        _levelManager.LoadAfterDelay("Core Gameplay", audioClip.length + 1);
    }
}