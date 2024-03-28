using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField] private AudioSource Audio;
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    
    public void StopStartPlaying(bool State)
    {
        if(State)
        {
            Audio.Play();
        }
        else if(!State)
        {
            Audio.Stop();
        }
    }
}
