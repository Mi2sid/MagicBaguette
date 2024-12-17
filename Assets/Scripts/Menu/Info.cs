using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    
    public AudioSource audioSource;

    public GameObject image;
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleImage(){
        audioSource.Play();
        state = !state;
        image.SetActive(state);
    }
}
