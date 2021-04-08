using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomPrice : MonoBehaviour
{
    public int monkeyPrice;
    public int dogPrice;
    public int fishPrice;
    public int godlingPrice;
    public int ghostPrice;
    private TextMeshProUGUI monkeyPriceText;
    private TextMeshProUGUI dogPriceText;
    private TextMeshProUGUI fishPriceText;
    private TextMeshProUGUI godlingPriceText;
    private TextMeshProUGUI ghostPriceText;

    private void Start()
    {
        monkeyPriceText = transform.Find("T_MonkeyFixed").GetComponent<TextMeshProUGUI>();
        dogPriceText = transform.Find("T_DogFixed").GetComponent<TextMeshProUGUI>();
        fishPriceText = transform.Find("T_FishFixed").GetComponent<TextMeshProUGUI>();
        godlingPriceText = transform.Find("T_GodlingFixed").GetComponent<TextMeshProUGUI>();
        ghostPriceText = transform.Find("T_GhostFixed").GetComponent<TextMeshProUGUI>();
        monkeyPriceText.text = monkeyPrice.ToString();
        dogPriceText.text = dogPrice.ToString();
        fishPriceText.text = fishPrice.ToString();
        godlingPriceText.text = godlingPrice.ToString();
        ghostPriceText.text = ghostPrice.ToString();
        print("Ué");
    }
}
