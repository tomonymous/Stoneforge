using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Crate : MonoBehaviour
{
    public GameObject crate;
    public GameObject crateImage;
    public GameObject ripple;
    public Vector2 pos;
    public float life;
    public int x;
    public int y;
    public GameObject[] text;

    //float originalY;
    //float originalX;

    public bool Initialize(Vector2 newPos, int pointX, int pointY, float l)
    {
        if (!FindObjectOfType<ComboMatch>().popups.Contains("x" + pointX + "y" + pointY))
        {
            FindObjectOfType<ComboMatch>().popups.Add("x" + pointX + "y" + pointY);
            //this.originalY = this.transform.position.y;
            //this.originalX = this.transform.position.x;

            pos = newPos;
            x = pointX;
            y = pointY;
            life = l;
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(originalX + ((float)Math.Sin(Time.time * 5) / 50), originalY + ((float)Math.Sin(Time.time * 3) / 20), transform.position.z); //(float)Math.Sin(Time.time*A)/B) A:Velocity, B:Distance
        if (life > 0)
        {
            life -= 1 * Time.deltaTime;
        }
        else
        {
            Destroy(crate);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        }
    }

    public void Clicked()
    {
        FindObjectOfType<ComboMatch>().audioManager.Play("BombDing");

        GameObject effectObj = Instantiate(ripple, FindObjectOfType<ComboMatch>().gameboard);      //ripple  effect
        RectTransform effectRect = effectObj.GetComponent<RectTransform>();
        effectRect.anchoredPosition = pos;
        Destroy(effectObj, 3f);

        int random = Random.Range(0, 100);
        if(random >= 0 && random < 1)
        {
            if(FindObjectOfType<ComboMatch>().skinChangeMoves > 0) //will not occur if skin already changed.
            {
                random = Random.Range(1, 100);
            }
            else
            {
                FindObjectOfType<ComboMatch>().TempSkinChange(4);
                FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
                FindObjectOfType<ComboMatch>().audioManager.Play("Confusion");
                Destroy(crate);
                return;
            }
        }
        if (random >= 1 && random < 5)
        {
            if (!(FindObjectOfType<ComboMatch>().zenMode || FindObjectOfType<ComboMatch>().chaosMode))
            {
                FindObjectOfType<ComboMatch>().timerEdits.Add(new TimerEdit(3f, 10));
                GameObject textObj = Instantiate(text[5], FindObjectOfType<ComboMatch>().gameboard);
                RectTransform textRect = textObj.GetComponent<RectTransform>();
                textRect.anchoredPosition = pos;
                Destroy(textObj, 3f);
                FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
                Destroy(crate);
                return;
            }
            else
            {
                random = Random.Range(10, 100);
            }
        }
        if (random >= 5 && random < 10)
        {
            if (!(FindObjectOfType<ComboMatch>().zenMode || FindObjectOfType<ComboMatch>().chaosMode))
            {
                FindObjectOfType<ComboMatch>().timerEdits.Add(new TimerEdit(-3f, 10));
                GameObject textObj = Instantiate(text[6], FindObjectOfType<ComboMatch>().gameboard);
                RectTransform textRect = textObj.GetComponent<RectTransform>();
                textRect.anchoredPosition = pos;
                Destroy(textObj, 3f);
                FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
                Destroy(crate);
                return;
            }
            else
            {
                random = Random.Range(10, 100);
            }
        }
        if (random >= 10 && random < 30)
        {
            FindObjectOfType<ComboMatch>().BombRush();
            GameObject textObj = Instantiate(text[1], FindObjectOfType<ComboMatch>().gameboard);      
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchoredPosition = pos;
            Destroy(textObj, 3f);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }
        if (random >= 30 && random < 40)
        {
            FindObjectOfType<ComboMatch>().TokenRush();
            GameObject textObj = Instantiate(text[2], FindObjectOfType<ComboMatch>().gameboard);
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchoredPosition = pos;
            Destroy(textObj, 3f);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }
        if (random >= 40 && random < 60)
        {
            FindObjectOfType<ComboMatch>().tokens += 5;
            GameObject textObj = Instantiate(text[3], FindObjectOfType<ComboMatch>().gameboard);
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchoredPosition = pos;
            Destroy(textObj, 3f);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }
        if (random >= 60 && random < 80)
        {
            FindObjectOfType<ComboMatch>().ExtraTurn(pos);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }

        if (random >= 80 && random < 90)
        {
            FindObjectOfType<ComboMatch>().ExplosiveTouch();
            GameObject textObj = Instantiate(text[7], FindObjectOfType<ComboMatch>().gameboard);
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchoredPosition = pos;
            Destroy(textObj, 3f);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }
        if (random >= 90 && random < 100)
        {
            FindObjectOfType<ComboMatch>().magicTouch = true;
            GameObject textObj = Instantiate(text[8], FindObjectOfType<ComboMatch>().gameboard);
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchoredPosition = pos;
            Destroy(textObj, 3f);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            Destroy(crate);
            return;
        }



    }
}
