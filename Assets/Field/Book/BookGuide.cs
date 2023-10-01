using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookGuide : MonoBehaviour
{
    [SerializeField] private AudioClip toggleClip;
    private CanvasGroup _canvasGroup;
    public bool _toggled;

    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Toggle(false);
    }

    public void OnToggle()
    {
        Toggle(!_toggled);
    }

    private void Toggle(bool toggle)
    {
        AudioSource.PlayClipAtPoint(toggleClip, Vector2.zero);
        _canvasGroup.interactable = toggle;
        _canvasGroup.blocksRaycasts = toggle;
        _canvasGroup.alpha = toggle ? 1 : 0;
        _toggled = toggle;
    }
}
