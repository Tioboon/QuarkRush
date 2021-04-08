using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollowBehaviour : MonoBehaviour
{
    private float lerpVar;
    public List<float> lerpBounds;
    public float lerpMod;
    private bool lerpGoingDown;
    public List<float> yBounds;
    private float yVar;
    public float yMod;
    private bool yGoingDown;
    public Vector2 distanceToInitPos;
    private Vector2 initPos;
    private GameObject _player;
    public float followEachXSec;

    private void Start()
    {
        _player = GameObject.Find("Player");
        lerpVar = lerpBounds[0];
    }

    private void FixedUpdate()
    {
        CalculateDistance();
        ChangeVarsValues();
        FollowPlayer();
    }

    private void CalculateDistance()
    {
        initPos = (Vector2)_player.transform.position - distanceToInitPos;
        initPos += new Vector2(0, yVar);
    }

    private void ChangeVarsValues()
    {
        if (!yGoingDown)
        {
            yVar += yMod;
            if (yVar > yBounds[1])
            {
                yGoingDown = true;
            }
        }
        else
        {
            yVar -= yMod;
            if (yVar < yBounds[0])
            {
                yGoingDown = false;
            }
        }
        
        if (!lerpGoingDown)
        {
            lerpVar += lerpMod;
            if (lerpVar > lerpBounds[1])
            {
                lerpGoingDown = true;
            }
        }
        else
        {
            lerpVar -= lerpMod;
            if (lerpVar < lerpBounds[0])
            {
                lerpGoingDown = false;
            }
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(initPos, _player.transform.position, lerpVar);
    }
}
