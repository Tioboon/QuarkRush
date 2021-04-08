using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetMovement : MonoBehaviour
{
    private float time;
    public float raio;
    public float velocity;
    public float x;
    public float y;

    void Update()
    {
        time += Time.deltaTime * velocity;
        x = Mathf.Cos(time) * raio;
        y = Mathf.Sin(time) * raio;
        transform.position = transform.parent.position + new Vector3(x, y, 0);
    }
}
