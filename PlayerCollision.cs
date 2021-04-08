using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Hand _hand;
    private GameVars _gameVars;
    private SoundEmmiter _soundEmmiter;
    public CustomPrice _customPrice;

    private void Start()
    {
        _hand = transform.parent.GetComponent<Hand>();
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        _soundEmmiter = Camera.main.transform.Find("Sounds").GetComponent<SoundEmmiter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RopeBase"))
        {
            _soundEmmiter.ExplosionSound();
            _hand.Die();
            var otherAnimator = other.GetComponent<Animator>();
            otherAnimator.Play("RopeExplosion");
            var timeOfAnimation = otherAnimator.GetCurrentAnimatorClipInfo(0).Length;
            other.GetComponent<Base>().DestroyThis();
        }

        if (other.CompareTag("Collectable"))
        {
            CollectableInfo otherInfo = other.GetComponent<CollectableInfo>();
            if (otherInfo.petType == "Dog")
            {
                _gameVars.dogsCollected += 1;
                if (_gameVars.dogsCollected >= _customPrice.dogPrice)
                {
                    _soundEmmiter.UnlockPetSound();
                }
                else
                {
                    _soundEmmiter.PetSound();
                }
                PlayerPrefs.SetInt("Dog", _gameVars.dogsCollected);
            }
            else if (otherInfo.petType == "Monkey")
            {
                _gameVars.monkeysCollected += 1;
                PlayerPrefs.SetInt("Monkey", _gameVars.monkeysCollected);
                if (_gameVars.monkeysCollected >= _customPrice.monkeyPrice)
                {
                    _soundEmmiter.UnlockPetSound();
                }
                else
                {
                    _soundEmmiter.PetSound();
                }
            }
            else if (otherInfo.petType == "Godling")
            {
                _gameVars.godlingsCollected += 1;
                PlayerPrefs.SetInt("Godling", _gameVars.godlingsCollected);
                if (_gameVars.godlingsCollected >= _customPrice.godlingPrice)
                {
                    _soundEmmiter.UnlockPetSound();
                }
                else
                {
                    _soundEmmiter.PetSound();
                }
            }
            else if (otherInfo.petType == "Ghost")
            {
                _gameVars.ghostCollected += 1;
                if (_gameVars.ghostCollected >= _customPrice.ghostPrice)
                {
                    _soundEmmiter.UnlockPetSound();
                }
                else
                {
                    _soundEmmiter.PetSound();
                }
                PlayerPrefs.SetInt("Ghost", _gameVars.ghostCollected);
            }
            else if (otherInfo.petType == "Fish")
            {
                _gameVars.fishCollected += 1;
                if (_gameVars.fishCollected >= _customPrice.fishPrice)
                {
                    _soundEmmiter.UnlockPetSound();
                }
                else
                {
                    _soundEmmiter.PetSound();
                }
                PlayerPrefs.SetInt("Fish", _gameVars.fishCollected);
            }
            Destroy(other.gameObject);
        }
    }
}
