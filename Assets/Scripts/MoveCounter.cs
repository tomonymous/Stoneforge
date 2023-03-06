using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounter : MonoBehaviour
{
    public static int movesMade = 0;
    public TextMeshProUGUI moves;

    void Start()
    {
    }

    void Update()
    {

        moves.text = movesMade.ToString("000");
    }
    public void Reset()
    {
        movesMade = 0;
    }
}
