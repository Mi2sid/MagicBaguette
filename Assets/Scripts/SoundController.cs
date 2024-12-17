using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public List<AudioSource> audios;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySpell(){
        audios[0].Play();
    }
    public void PlayClick(){
        audios[1].Play();
    }
    public void PlayError(){
        audios[2].Play();
    }
    public void PlayHurt(){
        audios[3].Play();
    }
}
