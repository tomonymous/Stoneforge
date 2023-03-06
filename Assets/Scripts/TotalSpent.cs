using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalSpent : MonoBehaviour
{
    Text totalSpent;
    public static int spent = 0;

    void Start()
    {
        totalSpent = GetComponent<Text>();
    }

    void Update()
    {

        totalSpent.text = spent.ToString("N0");
    }
}
