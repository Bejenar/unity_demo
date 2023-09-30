using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    private GridManager _gridManager;
    private InitialSpawner _spawner;
    private ScoreManager _scoreManager;

    public static int _level;

    private Dictionary<int, int> _levelUpRequirements;

    // Start is called before the first frame update
    void Start()
    {
        _level = 3;
        _gridManager = FindObjectOfType<GridManager>();
        _spawner = FindObjectOfType<InitialSpawner>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        
        _levelUpRequirements = new Dictionary<int, int>();
        _levelUpRequirements.Add(4, 500);
        _levelUpRequirements.Add(5, 1250);
        _levelUpRequirements.Add(6, 2500);
        _levelUpRequirements.Add(7, 5000);
        _levelUpRequirements.Add(8, 10000);
    }

    public void CheckForLevelUp()
    {
        var scoreTotal = _scoreManager.Score;
        
        if (!_levelUpRequirements.ContainsKey(_level + 1))
        {
            return;
        }

        var requiredScore = _levelUpRequirements[_level + 1];
        Debug.LogFormat("Requiring {0} for next level {1}. Current score {2}", requiredScore, _level +1, scoreTotal);
        if (requiredScore <= scoreTotal)
        {
            Debug.LogFormat("worthy of level up to {0}", _level + 1);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _level++;
        _gridManager.DestroyCells();
        if (_gridManager.size < 7)
        {
            _gridManager.size++;
        }

        _gridManager.Initialize();
        _spawner.PlantRandom();

        if (_level == 6)
        {
            _gridManager.SpawnObstacles(2);
        }

        if (_level == 7)
        {
            _gridManager.SpawnObstacles(4);
        }

        if (_level == 8)
        {
            Debug.Log("you won!");
        }
    }
}