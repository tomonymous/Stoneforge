using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnTracker : MonoBehaviour
{
    public static bool turnInProgress = false;
    public static bool triggeredByBomb = false;
    public static int turnsRemaining = 8;
    public TextMeshProUGUI turns;

    void Start()
    {
    }

    void Update()
    {
        turns.text = turnsRemaining.ToString("000");
    }

    public void Reset()
    {
        turnInProgress = false;
        turnsRemaining = 8;
    }
}
