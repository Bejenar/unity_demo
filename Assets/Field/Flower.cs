using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flower", menuName = "ScriptableObjects/Flower", order = 1)]
public class Flower : ScriptableObject
{
    public string flowerName;
    public Color color;
    public bool isObstacle;

    private static Dictionary<string, List<string>> _compatibilityMap;

    static Flower()
    {
        _compatibilityMap = new();
        _compatibilityMap.Add("Red", new List<string>(new[] { "Red", "Rose", "Orange" }));
        _compatibilityMap.Add("Orange", new List<string>(new[] { "Orange", "Red", "Blue" }));
        _compatibilityMap.Add("Blue", new List<string>(new[] { "Blue", "Orange", "Violet" }));
        _compatibilityMap.Add("Violet", new List<string>(new[] { "Violet", "Blue", "Rose" }));
        _compatibilityMap.Add("Rose", new List<string>(new[] { "Rose", "Violet", "Red" }));
    }

    public bool IsCompatible(Flower other)
    {
        if (other == null || other.isObstacle) return true;
        return _compatibilityMap[flowerName].Contains(other.flowerName) || IsSameFlower(other);
    }

    public bool IsSameFlower(Flower other)
    {
        return flowerName == other.flowerName;
    }
}