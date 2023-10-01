using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerObject : MonoBehaviour
{
    [SerializeField] private AudioClip growClip;
    [SerializeField] private AudioClip bloomClip;
    public Flower flower;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(growClip, Vector2.zero);
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(bloomClip, Vector2.zero);
    }
}
