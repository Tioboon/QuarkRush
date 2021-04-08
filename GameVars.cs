using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars : MonoBehaviour
{
    public int monkeysCollected;
    public int dogsCollected;
    public int godlingsCollected;
    public int fishCollected;
    public int ghostCollected;
    public int maxHighScore;
    public string selectedPet;
    public int withSound;

    private void Awake()
    {
        monkeysCollected = PlayerPrefs.GetInt("Monkey");
        dogsCollected = PlayerPrefs.GetInt("Dog");
        godlingsCollected = PlayerPrefs.GetInt("Godling");
        ghostCollected = PlayerPrefs.GetInt("Ghost");
        fishCollected = PlayerPrefs.GetInt("Fish");
        maxHighScore = PlayerPrefs.GetInt("HighScore");
        selectedPet = PlayerPrefs.GetString("Pet");
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        withSound = PlayerPrefs.GetInt("Sound");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ResetAllVars();
        }
    }

    private void ResetAllVars()
    {
        PlayerPrefs.DeleteAll();
    }
}
