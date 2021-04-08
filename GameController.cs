using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _gameController;

    public bool paused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_gameController == null)
        {
            _gameController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Pause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }
}
