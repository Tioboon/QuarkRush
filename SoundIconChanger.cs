using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIconChanger : MonoBehaviour
{
    public Sprite unMuted;
    public Sprite muted;
    private Image image;
    private GameVars _gameVars;
    public AudioSource _audioSourceMusic;

    private void Start()
    {
        image = GameObject.Find("Options").GetComponent<Image>();
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        if (_gameVars.withSound == 0)
        {
            image.sprite = muted;
        }
    }

    public void ChangeSprite()
    {
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        if(_gameVars.withSound == 0)
        {
            image.sprite = muted;
            StopMusic();
        }

        if (1 == _gameVars.withSound)
        {
            image.sprite = unMuted;
            StartMusic();
        }
    }
    
    public void ChangeSoundState()
    {
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        if (_gameVars.withSound == 1)
        {
            _gameVars.withSound = 0;
        }
        else
        {
            _gameVars.withSound = 1;
        }
        PlayerPrefs.SetInt("Sound", _gameVars.withSound);
    }

    public void StopMusic()
    {
        _audioSourceMusic.volume = 0;
    }

    private void StartMusic()
    {
        _audioSourceMusic.volume = 1;
    }
}
