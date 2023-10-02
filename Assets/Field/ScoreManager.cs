using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private UnityEvent unityEvent;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI animatedLabel;
    public int Score => score;

    private ScoreAnimator _scoreAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _scoreAnimator = FindObjectOfType<ScoreAnimator>();
        label.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore(int _score)
    {
        Pop(_score);
        score += _score;
        label.text = score.ToString();
        unityEvent.Invoke();
        Debug.Log("score is " + score);
    }

    private void Pop(int _score)
    {
        animatedLabel.text = $"+{_score}";
        _scoreAnimator.Pop();
    }
}