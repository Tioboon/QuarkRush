using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PontuationTracker : MonoBehaviour
{
    private Hand _playerHand;
    private TextMeshProUGUI _txt;

    private void Start()
    {
        _playerHand = GameObject.Find("Player").GetComponent<Hand>();
        _txt = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _txt.text = ((int)_playerHand.score).ToString();
    }
}
