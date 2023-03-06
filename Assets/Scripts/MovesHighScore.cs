using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovesHighScore : MonoBehaviour
{
    Text movesHighScore;

    void Start()
    {
        movesHighScore = GetComponent<Text>();
    }

    void Update()
    {

        movesHighScore.text = PlayerPrefs.GetInt("MovesHighScore", 0).ToString("000");
    }

}
