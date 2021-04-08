using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    public float lerpVariable;
    private Hand playerHand;
    public float maxZoomOut = 140;
    public float maxZoomIn = 80;
    private Camera _camera;
    public float sizeMod;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        lerpVariable *= Time.deltaTime;
        playerHand = player.GetComponent<Hand>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        var finalPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, finalPos, lerpVariable);
        if (playerHand.zoomCameraIn)
        {
            if(_camera.orthographicSize > maxZoomIn)
            {
                _camera.orthographicSize -= sizeMod;
            }
            else
            {
                playerHand.zoomCameraIn = false;
                _camera.orthographicSize = maxZoomIn;
            }
        }

        if (playerHand.zoomCameraOut)
        {
            if (_camera.orthographicSize < maxZoomOut)
            {
                _camera.orthographicSize += sizeMod;
            }
            else
            {
                playerHand.zoomCameraOut = false;
                _camera.orthographicSize = maxZoomOut;
            }
        }
    }
}
