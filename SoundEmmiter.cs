using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmmiter : MonoBehaviour
{
    public List<AudioClip> audios;

    private AudioSource playerSource;
    private AudioSource ropeSource;
    private AudioSource collectablesSource;

    private void Start()
    {
        playerSource = transform.Find("SoundMakerPlayer").GetComponent<AudioSource>();
        ropeSource = transform.Find("SoundMakerRope").GetComponent<AudioSource>();
        collectablesSource = transform.Find("SoundMakerCollectables").GetComponent<AudioSource>();
    }

    public void GrapRopeSound()
    {
        playerSource.clip = audios[0];
        playerSource.Play();
    }

    public void SwingRopeSound()
    {
        ropeSource.clip = audios[1];
        ropeSource.Play();
    }

    public void JumpSound()
    {
        playerSource.clip = audios[3];
        playerSource.Play();
    }

    public void ExplosionSound()
    {
        ropeSource.clip = audios[2];
        ropeSource.Play();
    }

    public void PetSound()
    {
        ropeSource.clip = audios[4];
        collectablesSource.Play();
    }

    public void UnlockPetSound()
    {
        ropeSource.clip = audios[5];
        collectablesSource.Play();
    }
}
