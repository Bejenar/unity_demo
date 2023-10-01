using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerObject : MonoBehaviour
{
    [SerializeField] private AudioClip growClip;
    [SerializeField] private AudioClip bloomClip;
    [SerializeField] private GameObject comboParticle;
    public Flower flower;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(growClip, Vector2.zero);
    }

    public void PreDestroy()
    {
        AudioSource.PlayClipAtPoint(bloomClip, Vector2.zero);
        var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1); 
        var obj = Instantiate(comboParticle, pos, Quaternion.identity);
        var particleSystem = obj.GetComponent<ParticleSystem>();
        
        var particleSystemMain = particleSystem.main;

        var color = flower.color;
        color.a = 1;
        
        particleSystemMain.startColor = color;
        
        particleSystem.Play();
        
        
    }
}
