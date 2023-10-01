using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitialSpawner : MonoBehaviour
{
    [SerializeField] private Flower[] flowers;
    
    private GridManager _gridManager;
    private List<Flower> _flowers;
    
    // Start is called before the first frame update
    void Start()
    {
        _gridManager = FindObjectOfType<GridManager>();

        _flowers = new List<Flower>(flowers);
        PlantRandom();
    }

    public void PlantRandom()
    {
        var flowersToPlant = Mathf.Clamp(LevelUpManager._level - 1, 2, 5);
        var toPlant = _flowers.Take(flowersToPlant).ToList();
        Debug.Log("Planting " + toPlant.Count);
        _gridManager.PlantFlowersAtRandomSpot(toPlant);
    }
}
