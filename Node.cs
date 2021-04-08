using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int number;
    public float xTolerance;
    public float worldLimitY;
    private Transform mainCam;


    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        if (transform.position.x < mainCam.position.x - xTolerance)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < worldLimitY)
        {
            Destroy(gameObject);
        }
    }
}
