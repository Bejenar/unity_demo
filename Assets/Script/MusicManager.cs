using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] levelMusicChange;

    private AudioSource _audioSource;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _audioSource.pitch = 1;
        var clipToPlay = levelMusicChange[scene.buildIndex];
        if (IsTheSameClipPlaying(clipToPlay)) return;
        Debug.LogFormat("current clip is {0}", clipToPlay);
        _audioSource.clip = clipToPlay;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    private bool IsTheSameClipPlaying(AudioClip clipToPlay)
    {
        // Unity Object overloads == 
        return clipToPlay == _audioSource.clip;
    }

    public void ChangeVolume(float volumeSliderValue)
    {
        _audioSource.volume = volumeSliderValue;
    }
}
