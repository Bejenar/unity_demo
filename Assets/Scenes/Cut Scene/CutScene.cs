using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{

    [SerializeField] private AudioClip glitchClip;


    public void PlayGlitch()
    {
        AudioSource.PlayClipAtPoint(glitchClip, Vector2.zero, 0.5f);
    }
}
