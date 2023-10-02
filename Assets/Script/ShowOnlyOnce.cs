using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyOnce : MonoBehaviour
{
    private static bool _show = true;
    private CanvasGroup _canvasGroup;
    
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Toggle(_show);
    }
    
    
    public void Toggle(bool toggle)
    {
        _canvasGroup.interactable = toggle;
        _canvasGroup.blocksRaycasts = toggle;
        _canvasGroup.alpha = toggle ? 1 : 0;
        _show = false;
    }
}
