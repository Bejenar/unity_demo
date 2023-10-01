using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float duration = 2f;
    [SerializeField] private bool autoStart = false;
    
    private Image _image;

    private void Start()
    {
        if (autoStart)
        {
            StartCoroutine(FadeIn());
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(FadeIn());
    }

    // Start is called before the first frame update
    public IEnumerator FadeIn()
    {
        _image = GetComponent<Image>();

        var timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            var progress = timeElapsed / duration;

            var currentValue = animationCurve.Evaluate(progress);

            var colorWithAlpha = _image.color;
            colorWithAlpha.a = 1 - currentValue;
            _image.color = colorWithAlpha;
            Debug.LogFormat("current alpha is {0}", colorWithAlpha.a);
            yield return null;
        }
    }
}