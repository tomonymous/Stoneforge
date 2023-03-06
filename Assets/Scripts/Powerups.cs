using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    public static Powerups instance;
    ComboMatch game;


    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        game = GetComponent<ComboMatch>();
    }
    //public bool ActivateBomb(Bomb b)
    //{
    //    if (game.armedBombStatus == 0 && game.gameRunning)
    //    {
    //        if(game.chaosMode) //can't use powerups in chaos mode.
    //        {
    //            return false;
    //        }
    //        FindObjectOfType<AudioManager>().Play("switch");
    //        b.img.color = new Color(1, 0.6f, 0.6f,1);
    //        game.ArmBomb(b);
    //        return true;
    //    }
    //    else if (FindObjectOfType<GameManager>().shopMenu.activeSelf)
    //    {
    //        Destroy(b.gameObject);
    //        game.recordInventory();
    //        PlayerPrefs.SetInt("TotalCash", PlayerPrefs.GetInt("TotalCash", 0) + game.bombPrices[b.id]);
    //        return false;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //public bool deActivateBomb(Bomb b)
    //{
    //    FindObjectOfType<AudioManager>().Play("switch");
    //    b.img.color = new Color(1, 1, 1, 1);
    //    game.disarmBomb();
    //    return true;
    //}
    
    
}
