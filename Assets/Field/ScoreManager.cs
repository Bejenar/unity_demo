using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private UnityEvent unityEvent;
    
    public int Score => score;
    
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int _score)
    {
        score += _score;
        unityEvent.Invoke();
        Debug.Log("score is " + score);
    }
}