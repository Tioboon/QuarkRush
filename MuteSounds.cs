using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSounds : MonoBehaviour
{
    private GameVars _gameVars;
    private AudioSource _audioSource;
    void Start()
    {
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        _audioSource = GetComponent<AudioSource>();
        if (_gameVars.withSound == 1)
        {
            _audioSource.volume = 1;
        }
        else
        {
            _audioSource.volume = 0;
        }
    }
}
