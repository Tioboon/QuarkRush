using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float velocityMod;
    
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _rigidbody2D.velocity -= Vector2.right * velocityMod;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _rigidbody2D.velocity += Vector2.right * velocityMod;
        }
    }
}
