using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    public Text lastMode;
    public Text lastPoints;
    public Text lastMoves;
    public Text movesScore1;
    public Text movesScore2;
    public Text movesScore3;
    public Text pointsScore1;
    public Text pointsScore2;
    public Text pointsScore3;
    public Text gamesPlayed;
    public Text tokens;
    public Text chaosMovesScore1;
    public Text chaosMovesScore2;
    public Text chaosMovesScore3;
    public Text chaosPointsScore1;
    public Text chaosPointsScore2;
    public Text chaosPointsScore3;

    public Text zenMovesScore1;
    public Text zenMovesScore2;
    public Text zenMovesScore3;
    public Text zenPointsScore1;
    public Text zenPointsScore2;
    public Text zenPointsScore3;

    public Text standardGamesPlayed;
    public Text zenGamesPlayed;
    public Text chaosGamesPlayed;


    public Text chaosGold;
    public Text totalMoves;
    public Text totalPoints;
    public Text goldCreated;

    public Text slowTime;
    public Text disableTimer;
    public Text blackBomb;
    public Text blackStop;
    public Text dotBomb;
    public Text extraTurn;
    public Text magicTouch;
    public Text crazyBomb;
    public Text goldBomb;
    public Text rowBomb;
    public Text disarm;
    public Text moreCrates;

    public GameObject confirmDelete;

    void Start()
    {
        lastMode.text = PlayerPrefs.GetString("LastMode", "Complete a Game!");
        lastPoints.text = PlayerPrefs.GetInt("LastPoints", 0).ToString("N0");
        lastMoves.text = PlayerPrefs.GetInt("LastMoves", 0).ToString("N0");

        movesScore1.text = PlayerPrefs.GetInt("MovesHighScore", 0).ToString("N0");
        movesScore2.text = PlayerPrefs.GetInt("MovesHighScore2", 0).ToString("N0");
        movesScore3.text = PlayerPrefs.GetInt("MovesHighScore3", 0).ToString("N0");
        pointsScore1.text = PlayerPrefs.GetInt("HighScore", 0).ToString("N0");
        pointsScore2.text = PlayerPrefs.GetInt("HighScore2", 0).ToString("N0");
        pointsScore3.text = PlayerPrefs.GetInt("HighScore3", 0).ToString("N0");
        gamesPlayed.text = PlayerPrefs.GetInt("GamesPlayed", 0).ToString("N0");
        tokens.text = PlayerPrefs.GetInt("TotalTokens", 0).ToString("N0");

        zenMovesScore1.text = PlayerPrefs.GetInt("ZenMovesHighScore", 0).ToString("N0");
        zenMovesScore2.text = PlayerPrefs.GetInt("ZenMovesHighScore2", 0).ToString("N0");
        zenMovesScore3.text = PlayerPrefs.GetInt("ZenMovesHighScore3", 0).ToString("N0");
        zenPointsScore1.text = PlayerPrefs.GetInt("ZenHighScore", 0).ToString("N0");
        zenPointsScore2.text = PlayerPrefs.GetInt("ZenHighScore2", 0).ToString("N0");
        zenPointsScore3.text = PlayerPrefs.GetInt("ZenHighScore3", 0).ToString("N0");

        chaosMovesScore1.text = PlayerPrefs.GetInt("ChaosMovesHighScore", 0).ToString("N0");
        chaosMovesScore2.text = PlayerPrefs.GetInt("ChaosMovesHighScore2", 0).ToString("N0");
        chaosMovesScore3.text = PlayerPrefs.GetInt("ChaosMovesHighScore3", 0).ToString("N0");
        chaosPointsScore1.text = PlayerPrefs.GetInt("ChaosHighScore", 0).ToString("N0");
        chaosPointsScore2.text = PlayerPrefs.GetInt("ChaosHighScore2", 0).ToString("N0");
        chaosPointsScore3.text = PlayerPrefs.GetInt("ChaosHighScore3", 0).ToString("N0");
        chaosGamesPlayed.text = PlayerPrefs.GetInt("ChaosGamesPlayed", 0).ToString("N0");
        chaosGold.text = PlayerPrefs.GetInt("ChaosGold", 0).ToString("N0");

        standardGamesPlayed.text = (PlayerPrefs.GetInt("GamesPlayed", 0) - PlayerPrefs.GetInt("ChaosGamesPlayed", 0) - PlayerPrefs.GetInt("ZenGamesPlayed", 0)).ToString("N0");
        zenGamesPlayed.text = PlayerPrefs.GetInt("ZenGamesPlayed", 0).ToString("N0");


        totalMoves.text = PlayerPrefs.GetInt("TotalMoves", 0).ToString("N0");
        totalPoints.text = PlayerPrefs.GetInt("TotalPoints", 0).ToString("N0");
        goldCreated.text = PlayerPrefs.GetInt("GoldCreated", 0).ToString("N0");

        crazyBomb.text = PlayerPrefs.GetInt("CrazyBomb", 0).ToString("N0");
        slowTime.text = PlayerPrefs.GetInt("SlowTime", 0).ToString("N0");
        rowBomb.text = PlayerPrefs.GetInt("RowBomb", 0).ToString("N0");
        blackStop.text = PlayerPrefs.GetInt("BlackStop", 0).ToString("N0");
        blackBomb.text = PlayerPrefs.GetInt("BlackBomb", 0).ToString("N0");
        dotBomb.text = PlayerPrefs.GetInt("DotBomb", 0).ToString("N0");
        goldBomb.text = PlayerPrefs.GetInt("GoldBomb", 0).ToString("N0");
        disableTimer.text = PlayerPrefs.GetInt("DisableTimer", 0).ToString("N0");
        extraTurn.text = PlayerPrefs.GetInt("ExtraTurn", 0).ToString("N0");
        magicTouch.text = PlayerPrefs.GetInt("MagicTouch", 0).ToString("N0");
        disarm.text = PlayerPrefs.GetInt("Disarm", 0).ToString("N0");
        moreCrates.text = PlayerPrefs.GetInt("MoreCrates", 0).ToString("N0");
        confirmDelete.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmDelete()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstTime", 1);
    }

    public void DeleteAll()
    {
        confirmDelete.SetActive(true);
    }
}
