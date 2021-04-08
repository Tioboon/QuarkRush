using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
        if (_gameController.paused)
        {
            _gameController.paused = false;
        }
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
