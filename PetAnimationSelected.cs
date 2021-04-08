using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimationSelected : MonoBehaviour
{
    private GameVars _gameVars;
    private Animator _animator;

    private void Start()
    {
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        _animator = GetComponent<Animator>();
        _animator.Play(_gameVars.selectedPet);
        if (_gameVars.selectedPet == "Fish")
        {
            transform.Find("Helmet").gameObject.SetActive(true);
        }
        if (_gameVars.selectedPet != "Monkey" && _gameVars.selectedPet != "Dog" && _gameVars.selectedPet != "Fish" && _gameVars.selectedPet != "Ghost" && _gameVars.selectedPet != "Godling")
        {
            gameObject.SetActive(false);
        }
    }
}
