using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomNumbers : MonoBehaviour
{
    private GameVars _gvars;
    private TextMeshProUGUI monkeyT;
    private TextMeshProUGUI dogT;
    private TextMeshProUGUI fishT;
    private TextMeshProUGUI ghostT;
    private TextMeshProUGUI godlingT;

    private void Start()
    {
        _gvars = GameObject.Find("GameController").GetComponent<GameVars>();
        monkeyT = transform.Find("T_Monkey").GetComponent<TextMeshProUGUI>();
        ghostT = transform.Find("T_Ghost").GetComponent<TextMeshProUGUI>();
        godlingT = transform.Find("T_Godling").GetComponent<TextMeshProUGUI>();
        dogT = transform.Find("T_Dog").GetComponent<TextMeshProUGUI>();
        fishT = transform.Find("T_Fish").GetComponent<TextMeshProUGUI>();
        monkeyT.text = _gvars.monkeysCollected.ToString();
        ghostT.text =  _gvars.ghostCollected.ToString();
        fishT.text =  _gvars.fishCollected.ToString();
        dogT.text =  _gvars.dogsCollected.ToString();
        godlingT.text = _gvars.godlingsCollected.ToString();
    }
}
