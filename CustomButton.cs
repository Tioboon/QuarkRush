using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    private GameVars _gameVars;
    private CustomPrice _customPrice;
    public Image boxOne;
    public Sprite boxOneSelected;
    public Image boxTwo;
    public Sprite boxTwoSelected;
    public Image boxThree;
    public Sprite boxThreeSelected;
    public Image boxFour;
    public Sprite boxFourSelected;
    public Image boxFive;
    public Sprite boxFiveSelected;
    private Image monkey;
    private Image dog;
    private Image fish;
    private Image ghost;
    private Image godling;
    public Sprite monkeyLibSprite;
    public Sprite dogLibSprite;
    public Sprite fishLibSprite;
    public Sprite ghostLibSprite;
    public Sprite godlingLibSprite;

    public Sprite initialBoxTwo;
    public Sprite initialBoxOne;
    public Sprite initialBoxThree;
    public Sprite initialBoxFour;
    public Sprite initialBoxFive;
    
    
    private bool monkeyLib;
    private bool dogLib;
    private bool fishLib;
    private bool ghostLib;
    private bool godlingLib;

    private Image selectedBox;
    private int selectedInt;


    private void Start()
    {
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        _customPrice = transform.Find("FixedNumbers").GetComponent<CustomPrice>();
        monkey = transform.Find("Monkey").GetComponent<Image>();
        dog = transform.Find("Dog").GetComponent<Image>();
        fish = transform.Find("Fish").GetComponent<Image>();
        ghost = transform.Find("Ghost").GetComponent<Image>();
        godling = transform.Find("Godling").GetComponent<Image>();
        CheckLiberated();
        CheckStartSelected();
    }

    private void CheckStartSelected()
    {
        switch (_gameVars.selectedPet)
        {
            case "Monkey":
                boxOne.sprite = boxOneSelected;
                selectedBox = boxOne;
                selectedInt = 1; return;
            case "Dog":
                boxTwo.sprite = boxTwoSelected;
                selectedBox = boxTwo;
                selectedInt = 2; return;
            case "Fish":
                boxFour.sprite = boxFourSelected;
                selectedBox = boxFour;
                selectedInt = 4; return;
            case "Ghost":
                boxThree.sprite = boxThreeSelected;
                selectedBox = boxThree;
                selectedInt = 3; return;
            case "Godling":
                boxFive.sprite = boxFiveSelected;
                selectedBox = boxFive;
                selectedInt = 5; return;
        }
    }
    
    private void CheckLiberated()
    {
        if (_gameVars.monkeysCollected >= _customPrice.monkeyPrice)
        {
            monkeyLib = true;
            monkey.sprite = monkeyLibSprite;
        }

        if (_gameVars.dogsCollected >= _customPrice.dogPrice)
        {
            dogLib = true;
            dog.sprite = dogLibSprite;
        }
        
        if (_gameVars.fishCollected >= _customPrice.fishPrice)
        {
            fishLib = true;
            fish.sprite = fishLibSprite;
        }
        
        if (_gameVars.ghostCollected >= _customPrice.ghostPrice)
        {
            ghostLib = true;
            ghost.sprite = ghostLibSprite;
        }
        
        if (_gameVars.godlingsCollected >= _customPrice.godlingPrice)
        {
            godlingLib = true;
            godling.sprite = godlingLibSprite;
        }
    }
    
    private void DeselectBox()
    {
        if (selectedBox != null)
        {
            switch (selectedInt)
            {
                case 1 :
                    selectedBox.sprite = initialBoxOne; return;
                case 2:
                    selectedBox.sprite = initialBoxTwo; return;
                case 3:
                    selectedBox.sprite = initialBoxThree; return;
                case 4:
                    selectedBox.sprite = initialBoxFour; return;
                case 5:
                    selectedBox.sprite = initialBoxFive; return;
            }
            
        }
    }

    public void CheckMonkey()
    {
        if (monkeyLib)
        {
            boxOne.sprite = boxOneSelected;
            
            DeselectBox();

            selectedBox = boxOne;
            selectedInt = 1;

            _gameVars.selectedPet = "Monkey";
            PlayerPrefs.SetString("Pet", _gameVars.selectedPet);
        }
        else
        {
            print("Você não coletou macacos o suficente");
        }
    }

    public void CheckDog()
    {
        if (dogLib)
        {
            boxTwo.sprite = boxTwoSelected;
            DeselectBox();
            selectedBox = boxTwo;
            selectedInt = 2;
            _gameVars.selectedPet = "Dog";
            PlayerPrefs.SetString("Pet", _gameVars.selectedPet);
        }
        else
        {
            print("Você não coletou Chorrinhos o suficente");
        }
    }

    public void CheckFish()
    {
        if (fishLib)
        {
            boxFour.sprite = boxFourSelected;
            DeselectBox();
            selectedBox = boxFour;
            selectedInt = 4;
            _gameVars.selectedPet = "Fish";
            PlayerPrefs.SetString("Pet", _gameVars.selectedPet);
        }
        else
        {
            print("Você não coletou Peixes o suficente");
        }
    }
    
    public void CheckGhost()
    {
        if (ghostLib)
        {
            boxThree.sprite = boxThreeSelected;
            DeselectBox();
            selectedBox = boxThree;
            selectedInt = 3;
            _gameVars.selectedPet = "Ghost";
            PlayerPrefs.SetString("Pet", _gameVars.selectedPet);
        }
        else
        {
            print("Você não coletou Fantasmas o suficente");
        }
    }
    public void CheckGodling()
    {
        if (godlingLib)
        {
            boxFive.sprite = boxFiveSelected;
            DeselectBox();
            selectedBox = boxFive;
            selectedInt = 5;
            _gameVars.selectedPet = "Godling";
            PlayerPrefs.SetString("Pet", _gameVars.selectedPet);
        }
        else
        {
            print("Você não coletou Fetos o suficente");
        }
    }
}
