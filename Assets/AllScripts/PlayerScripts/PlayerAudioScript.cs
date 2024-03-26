using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{

    [Header("Core")]
    private AudioSource AS;
    [Header("Sounds")]
    [SerializeField] private AudioClip FireballCastSFX;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AudioFireballCastingSound()
    {
        AS.clip = FireballCastSFX;
        AS.Play();
    }
}
