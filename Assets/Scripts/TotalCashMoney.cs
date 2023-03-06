using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCashMoney : MonoBehaviour
{
    Text totalCash;

    void Start()
    {
        totalCash = GetComponent<Text>(); 
    }

    void Update()
    {

        totalCash.text = PlayerPrefs.GetInt("TotalCash", 0).ToString("C0");
    }

}
