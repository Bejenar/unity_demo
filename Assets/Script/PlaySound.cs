using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    [SerializeField] private AudioClip audioClip;

    public void Play()
    {
        AudioSource.PlayClipAtPoint(audioClip, Vector2.zero);
    }
}
