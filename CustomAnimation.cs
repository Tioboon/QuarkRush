using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAnimation : MonoBehaviour
{
    private Image bg;
    public float bgAlphaMod;
    public float execAlphaTime;
    private float bgAlpha;
    public RectTransform boxOne;
    public RectTransform boxTwo;
    public RectTransform boxThree;
    public RectTransform boxFour;
    public RectTransform boxFive;
    public float distanceMove;
    public float timeForFirstBoxes;
    public float timeForSecondBoxes;

    private float lerpVarOne;
    public float lerpMod;

    private float lerpVarTwo;
    private float lerpVarThree;

    public RectTransform Monkey;
    public RectTransform Dog;
    public RectTransform Ghost;
    public RectTransform Fish;
    public RectTransform Godling;

    public Transform monkeyText;
    public Transform monkeyTextFixed;
    public Transform dogText;
    public Transform dogTextFixed;
    public Transform ghostText;
    public Transform ghostTextFixed;
    public Transform fishText;
    public Transform fishTextFixed;
    public Transform godlingText;
    public Transform godlingTextFixed;

    public GameObject buttons;
    public void OnEnable()
    {
        lerpVarOne = 0;
        lerpVarTwo = 0;
        lerpVarThree = 0;
        bgAlpha = 0;
        boxOne.gameObject.SetActive(false);
        boxTwo.gameObject.SetActive(false);
        boxThree.gameObject.SetActive(false);
        boxFour.gameObject.SetActive(false);
        boxFive.gameObject.SetActive(false);
        Monkey.gameObject.SetActive(false);
        monkeyText.gameObject.SetActive(false);
        monkeyTextFixed.gameObject.SetActive(false);
        Dog.gameObject.SetActive(false);
        dogText.gameObject.SetActive(false);
        dogTextFixed.gameObject.SetActive(false);
        Ghost.gameObject.SetActive(false);
        ghostText.gameObject.SetActive(false);
        ghostTextFixed.gameObject.SetActive(false);
        Fish.gameObject.SetActive(false);
        fishText.gameObject.SetActive(false);
        fishTextFixed.gameObject.SetActive(false);
        Godling.gameObject.SetActive(false);
        godlingText.gameObject.SetActive(false);
        godlingTextFixed.gameObject.SetActive(false);
        buttons.gameObject.SetActive(false);
        FadeIn();
    }

    private void FadeIn()
    {
        bg = transform.Find("BG").GetComponent<Image>();
        StartCoroutine(FadeInCorout());
    }

    private IEnumerator FadeInCorout()
    {
        var actualColor = bg.color;
        actualColor.a = bgAlpha;
        bgAlpha += bgAlphaMod;
        bg.color = actualColor;
        if (bgAlpha < 1)
        {
            yield return new WaitForSeconds(execAlphaTime);
            StartCoroutine(FadeInCorout());
        }
        else
        {
            StartCoroutine(MoveComponents());
        }
    }

    private IEnumerator MoveComponents()
    {
        var initOne = boxOne.transform.position;
        var initTwo = boxTwo.transform.position;
        var initThree = boxThree.transform.position;
        var initFour = boxFour.transform.position;
        var initFive = boxFour.transform.position;
        boxOne.transform.position += new Vector3(-distanceMove, 0);
        boxFour.transform.position += new Vector3(+distanceMove, 0);
        boxTwo.transform.position += new Vector3(-distanceMove, 0);
        boxThree.transform.position += new Vector3(+distanceMove, 0);
        boxFive.transform.position += new Vector3(0, distanceMove);
        boxOne.gameObject.SetActive(true);
        boxTwo.gameObject.SetActive(true);
        boxThree.gameObject.SetActive(true);
        boxFour.gameObject.SetActive(true);
        boxFive.gameObject.SetActive(true);
        StartCoroutine(MoveComponentsOne(initOne, initFour, boxOne.transform.position, boxFour.transform.position));
        yield return new WaitForSeconds(timeForFirstBoxes);
        StartCoroutine(MoveComponentsTwo(initTwo, initThree, boxTwo.transform.position, boxThree.transform.position));
        yield return new WaitForSeconds(timeForSecondBoxes);
        StartCoroutine(MoveComponentsThree(initFive, boxFive.transform.position));
    }

    private IEnumerator MoveComponentsOne(Vector2 initPosBoxOne, Vector2 initPosBoxFour, Vector2 finalPosBoxOne, Vector2 finalPosBoxFour)
    {
        boxOne.transform.position = Vector3.Lerp(finalPosBoxOne, initPosBoxOne, lerpVarOne);
        boxFour.transform.position = Vector3.Lerp(finalPosBoxFour, initPosBoxFour, lerpVarOne);
        lerpVarOne += lerpMod;
        yield return new WaitForSeconds(execAlphaTime);
        if (lerpVarOne < 1)
        {
            StartCoroutine(MoveComponentsOne(initPosBoxOne, initPosBoxFour, finalPosBoxOne, finalPosBoxFour));
        }
        else
        {
            Monkey.gameObject.SetActive(true);
            Fish.gameObject.SetActive(true);
            monkeyText.gameObject.SetActive(true);
            monkeyTextFixed.gameObject.SetActive(true);
            fishText.gameObject.SetActive(true);
            fishTextFixed.gameObject.SetActive(true);
        }
    }
    
    private IEnumerator MoveComponentsTwo(Vector2 initPosBoxOne, Vector2 initPosBoxFour, Vector2 finalPosBoxOne, Vector2 finalPosBoxFour)
    {
        boxTwo.transform.position = Vector3.Lerp(finalPosBoxOne, initPosBoxOne, lerpVarTwo);
        boxThree.transform.position = Vector3.Lerp(finalPosBoxFour, initPosBoxFour, lerpVarTwo);
        lerpVarTwo += lerpMod;
        yield return new WaitForSeconds(execAlphaTime);
        if (lerpVarTwo < 1)
        {
            StartCoroutine(MoveComponentsTwo(initPosBoxOne, initPosBoxFour, finalPosBoxOne, finalPosBoxFour));
        }
        else
        {
            Dog.gameObject.SetActive(true);
            Ghost.gameObject.SetActive(true);
            dogText.gameObject.SetActive(true);
            dogTextFixed.gameObject.SetActive(true);
            ghostText.gameObject.SetActive(true);
            ghostTextFixed.gameObject.SetActive(true);
        }
    }

    private IEnumerator MoveComponentsThree(Vector3 initPos, Vector3 finalPos)
    {
        boxFive.transform.position = Vector3.Lerp(finalPos, initPos, lerpVarThree);
        lerpVarThree += lerpMod;
        yield return new WaitForSeconds(execAlphaTime);
        if (lerpVarThree < 1)
        {
            StartCoroutine(MoveComponentsThree(initPos,finalPos));
        }
        else
        {
            Godling.gameObject.SetActive(true);
            godlingText.gameObject.SetActive(true);
            godlingTextFixed.gameObject.SetActive(true);
            buttons.SetActive(true);
        }
    }
}
