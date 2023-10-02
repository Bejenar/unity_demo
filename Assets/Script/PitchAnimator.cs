using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PitchAnimator : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float duration = 2f;
    [SerializeField] private bool autoStart = false;
    [SerializeField] private float delay = 1f;
    private AudioSource _audioSource;

    private void Start()
    {
        // Cursor.visible = false;
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
        _audioSource = FindObjectOfType<AudioSource>();

        yield return new WaitForSeconds(delay);

        var timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            var progress = timeElapsed / duration;

            var currentValue = animationCurve.Evaluate(progress);

            _audioSource.pitch = currentValue;
            yield return null;
        }

        _audioSource.clip = null;
    }
}