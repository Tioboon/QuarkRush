using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasAnimation : MonoBehaviour
{
    private RectTransform fade;
    private RectTransform play;
    private RectTransform custom;
    private RectTransform options;
    private RectTransform title;

    private Vector2 finalPos;

    public float dislocation;

    private float lerpVarOne;
    private float lerpVarTwo;
    private float lerpVarThree;
    [FormerlySerializedAs("lerpVarModifier")] public float modLerp;
    public float modifierOfModLerp;

    public float attEachXSec;

    private bool calledCustom;
    private bool calledOptions;

    private bool bouncePlay;
    private bool alreadyBouncePlay;
    private bool bounceCustom;

    private void Start()
    {
        StartAnimation();
    }

    public void StartAnimation()
    {
        GetReferences();
        finalPos = Vector2.zero;
        fade.anchoredPosition = new Vector2(0, dislocation);
        title.anchoredPosition = new Vector2(0, dislocation);
        custom.anchoredPosition = new Vector2(-dislocation, 0);
        play.anchoredPosition = new Vector2(-dislocation, 0);
        options.anchoredPosition = new Vector2(dislocation, 0);
        lerpVarOne = 0;
        lerpVarTwo = 0;
        lerpVarThree = 0;
        bouncePlay = false;
        alreadyBouncePlay = false;
        bounceCustom = false;
        calledCustom = false;
        calledOptions = false;
        StartCoroutine(Title(fade.anchoredPosition, play.anchoredPosition, options.anchoredPosition));
    }

    private void GetReferences()
    {
        fade = transform.Find("Fade").GetComponent<RectTransform>();
        play = transform.Find("Play").GetComponent<RectTransform>();
        custom = transform.Find("Custom").GetComponent<RectTransform>();
        options = transform.Find("Options").GetComponent<RectTransform>();
        title = transform.Find("Title").GetComponent<RectTransform>();
    }

    private IEnumerator Title(Vector2 initPos, Vector2 secondInitPos, Vector2 thirdInitPos)
    {
        fade.anchoredPosition = Vector2.Lerp(initPos, finalPos, lerpVarOne);
        title.anchoredPosition = Vector2.Lerp(initPos, finalPos, lerpVarOne);
        lerpVarOne += modLerp;
        yield return new WaitForSeconds(attEachXSec);
        if (lerpVarOne < 1)
        {
            StartCoroutine(Title(initPos, secondInitPos, thirdInitPos));
        }
        else
        {
            lerpVarOne = 0;
            StartCoroutine(Play(secondInitPos, thirdInitPos));
        }
    }

    private IEnumerator Play(Vector2 secondInitPos, Vector2 thirdInitPos)
    {
        play.anchoredPosition = Vector2.Lerp(secondInitPos, finalPos, lerpVarOne);
        if (bouncePlay)
        {
            lerpVarOne -= modLerp;
            modLerp -= modifierOfModLerp;
        }
        else
        {
            lerpVarOne += modLerp;
            if (alreadyBouncePlay)
            {
                modLerp += modifierOfModLerp;
            }
        }

        yield return new WaitForSeconds(attEachXSec);
        
        if (lerpVarOne > 0.5f && !calledCustom)
        {
            calledCustom = true;
            StartCoroutine(Custom(secondInitPos));
        }

        if (lerpVarOne > 0.75f && !calledOptions)
        {
            calledOptions = true;
            StartCoroutine(Options(thirdInitPos));
        }

        
        if (bouncePlay)
        {
            if (lerpVarOne < 0.8f)
            {
                bouncePlay = false;
                alreadyBouncePlay = true;
                StartCoroutine(Play(secondInitPos, thirdInitPos));
            }
            else
            {
                StartCoroutine(Play(secondInitPos, thirdInitPos));
            }
        }
        else
        {
            if (lerpVarOne < 1)
            {
                StartCoroutine(Play(secondInitPos, thirdInitPos));
            }
            else
            {
                if (!alreadyBouncePlay)
                {
                    StartCoroutine(Play(secondInitPos, thirdInitPos));
                    bouncePlay = true;
                }
            }
        }
    }

    private IEnumerator Custom(Vector2 secondInitPos)
    {
        custom.anchoredPosition = Vector2.Lerp(secondInitPos, finalPos, lerpVarTwo);
        lerpVarTwo += modLerp;
        yield return new WaitForSeconds(attEachXSec);
        if (lerpVarTwo < 1)
        {
            StartCoroutine(Custom(secondInitPos));
        }
    }

    private IEnumerator Options(Vector3 thirdInitPos)
    {
        options.anchoredPosition = Vector2.Lerp(thirdInitPos, finalPos, lerpVarThree);
        lerpVarThree += modLerp;
        yield return new WaitForSeconds(attEachXSec);
        if (lerpVarThree < 1)
        {
            StartCoroutine(Options(thirdInitPos));
        }
    }
}
