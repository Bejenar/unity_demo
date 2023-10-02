using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MousceParticles : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem;

    private Camera _cam;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Init;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Init;
    }

    void Init(Scene scene, LoadSceneMode mode)
    {
        DontDestroyOnLoad(gameObject);
        _cam = FindObjectOfType<Camera>();
    }

    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Cut Scene") return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Cell.clickedThisFrame)
            {
                Cell.clickedThisFrame = false;
                return;
            }
            var worldPosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(_particleSystem, worldPosition, Quaternion.identity);
        }
    }
}
