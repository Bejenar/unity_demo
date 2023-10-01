using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float duration = 2f;

    private Image _image;

    // Start is called before the first frame update
    IEnumerator Start()
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