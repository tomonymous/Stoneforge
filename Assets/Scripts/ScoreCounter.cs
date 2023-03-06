using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score = 0;
    Text showScore;

    void Start()
    {
        showScore = GetComponent<Text>();
    }

    void Update()
    {

        showScore.text = score.ToString("N0");
    }
    public void Reset()
    {
        score = 0;
    }
}
