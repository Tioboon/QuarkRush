using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableInfo : MonoBehaviour
{
    public string petType;
    public List<String> clipsName;
    private Animator _animator;
    private GameVars _gameVars;
    private CustomPrice _customPrice;
    public GameObject CCanvas;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        var interactables = CCanvas.transform.Find("Interactables");
        var fixedNumbers = interactables.Find("FixedNumbers");
        _customPrice = fixedNumbers.GetComponent<CustomPrice>();
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        var randomClip = Random.Range(0, clipsName.Count);
        bool monkeyLiberated = false;
        bool dogLiberated = false;
        bool ghostLiberated = false;
        bool fishLiberated = false;
        bool godlingLiberated = false;
        
        if (clipsName[randomClip] == "Monkey" && _gameVars.monkeysCollected >= _customPrice.monkeyPrice)
        {
            monkeyLiberated = true;
            randomClip += 1;
        }
        
        if (clipsName[randomClip] == "Dog" && _gameVars.dogsCollected >= _customPrice.dogPrice)
        {
            dogLiberated = true;
            randomClip += 1;
        }

        if (clipsName[randomClip] == "Ghost" && _gameVars.ghostCollected >= _customPrice.ghostPrice)
        {
            ghostLiberated = true;
            randomClip += 1;
        }

        if (clipsName[randomClip] == "Fish" && _gameVars.fishCollected >= _customPrice.fishPrice)
        {
            fishLiberated = true;
            randomClip += 1;
        }

        if (clipsName[randomClip] == "Godling" && _gameVars.godlingsCollected >= _customPrice.godlingPrice)
        {
            godlingLiberated = true;
            randomClip = 0;
        }

        if (monkeyLiberated && dogLiberated && ghostLiberated && fishLiberated && godlingLiberated)
        {
            Destroy(gameObject);
        }
        
        _animator.Play(clipsName[randomClip]);
        petType = clipsName[randomClip];
    }
}
