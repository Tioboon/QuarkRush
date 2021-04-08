using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocity;
    public Vector2 worldSize;
    public float respiro;
    public float listMaxLenght;
    public float limitToSpawnNew;
    public float y_pos;
    private Vector2 worldSpriteSize;
    private Transform _transform;
    private List<Transform> listGo = new List<Transform>();
    private bool spawnChecked;
    private SpriteRenderer _spriteRenderer;
    private Vector2 screenSizePixels;
    private Vector2 floorSizePixels;
    private float initLimitToSpawn;
    private GameObject _camera;

    private void Start()
    {
        initLimitToSpawn = limitToSpawnNew;
        _transform = transform.Find("1");
        if (_transform == null)
        {
            _transform = transform.Find("2");
            if (_transform == null)
            {
                _transform = transform.Find("3");
                if (_transform == null)
                {
                    _transform = transform.Find("4");
                    if (_transform == null)
                    {
                        _transform = transform.Find("5");
                        if (_transform == null)
                        {
                            _transform = transform.Find("6");
                        }
                    }
                }
            }
        }
        _transform = transform.GetChild(0);
        _camera = Camera.main.gameObject;
        _spriteRenderer = _transform.GetComponent<SpriteRenderer>();
        screenSizePixels = new Vector2(Screen.width, Screen.height);
        floorSizePixels = GetSizeOfObjectInPixel(_spriteRenderer);
        var initPos = new Vector3(-worldSize.x / 2, y_pos);
        listMaxLenght = (screenSizePixels.x / floorSizePixels.x + respiro)*2;
        GeneratePlainWorld(screenSizePixels, floorSizePixels, initPos);
        
        InvokeRepeating("CheckCameraDistance", 0 , 0.2f);
    }

    private void Update()
    {
        if (spawnChecked)
        {
            CheckLastPosition(listGo.Count - 1);
        }
    }

    private Vector2 GetSizeOfObjectInPixel(SpriteRenderer spriteRenderer)
    {
        Vector2 spriteSize = spriteRenderer.sprite.rect.size;
        Vector2 localSpriteSize = spriteSize / spriteRenderer.sprite.pixelsPerUnit;
        Vector3 worldSize = new Vector3(localSpriteSize.x, localSpriteSize.y, 0);
        worldSize.x *= transform.lossyScale.x;
        worldSize.y *= transform.lossyScale.y;
        worldSpriteSize = localSpriteSize;

        Vector3 screen_size = 0.5f * worldSize / Camera.main.orthographicSize;
        screen_size.y *= Camera.main.aspect;

        Vector3 in_pixels = new Vector3(screen_size.x * Camera.main.pixelWidth, screen_size.y * Camera.main.pixelHeight, 0) * 0.5f;
        return new Vector2(in_pixels.x, in_pixels.y);
    }
    
    private void GeneratePlainWorld(Vector2 screenSize, Vector2 objectSize, Vector2 initialPos)
    {
        float floorsNumber = screenSize.x / objectSize.x + respiro;
        for (int i = 0; i < floorsNumber; i++)
        {
            var newFloor = Instantiate(_transform, transform.parent, true);
            newFloor.transform.position = initialPos + new Vector2(_transform.transform.lossyScale.x * worldSpriteSize.x, 0) * i;
            newFloor.gameObject.SetActive(true);
            listGo.Add(newFloor);
            if (listGo.Count > listMaxLenght)
            {
                Destroy(listGo[0].gameObject);
                listGo.Remove(listGo[0]);
            }
        }

        spawnChecked = true;
    }
    
    private void CheckLastPosition(int nFloor)
    {
        var floorObj = listGo[nFloor];
        if (floorObj.transform.position.x < limitToSpawnNew)
        {
            var initPos = new Vector2(floorObj.transform.position.x, y_pos);
            spawnChecked = false;
            GeneratePlainWorld(screenSizePixels, floorSizePixels, initPos);
        }
    }
    
    private void CheckCameraDistance()
    {
        limitToSpawnNew = _camera.transform.position.x + initLimitToSpawn;
    }
}
