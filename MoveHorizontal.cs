using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    public Vector2 velocity;

    private void Update()
    {
        transform.position += (Vector3)velocity;
    }
}
