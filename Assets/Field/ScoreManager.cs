using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private UnityEvent unityEvent;
    [SerializeField] private TextMeshProUGUI label;
    
    public int Score => score;
    
    

    // Start is called before the first frame update
    void Start()
    {
        label.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int _score)
    {
        score += _score;
        label.text = score.ToString();
        unityEvent.Invoke();
        Debug.Log("score is " + score);
    }
}