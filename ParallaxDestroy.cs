using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDestroy : MonoBehaviour
{
    private Transform cameraT;
    public float limit;
    public float execTime;


    private void Start()
    {
        cameraT = Camera.main.transform;
    }

    private IEnumerator Check()
    {
        CheckDistanceFromCamera();
        yield return new WaitForSeconds(execTime);
        StartCoroutine(Check());
    }

    private void CheckDistanceFromCamera()
    {
        if (transform.position.x < cameraT.transform.position.x - limit)
        {
            Destroy(gameObject);
            print("Destruido: " + name);
        }
    }
}
