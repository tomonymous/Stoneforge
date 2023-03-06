using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TempPowerUp : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject tempPowerUp;
    public Image powerUpImage;
    public int id;
    public float life;
    public int x;
    public int y;

    float originalY;
    float originalX;

    public bool Initialize(int pointX, int pointY, float l, Sprite s, int i)
    {
        if (!FindObjectOfType<ComboMatch>().popups.Contains("x" + pointX + "y" + pointY))
        {
           // powerUpImage = GetComponent<Image>();
            powerUpImage.sprite = s;
            this.originalY = this.transform.position.y;
            this.originalX = this.transform.position.x;
            //text.text = life.ToString("n0");
            text.text = "";
            life = l;
            id = i;
            x = pointX;
            y = pointY;
            FindObjectOfType<ComboMatch>().popups.Add("x" + x + "y" + y);
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(originalX + ((float)Math.Sin(Time.time * 5) / 50), originalY + ((float)Math.Sin(Time.time * 3) / 20), transform.position.z); //(float)Math.Sin(Time.time*A)/B) A:Velocity, B:Distance

        if (life > 0)
        {
            life -= 1 * Time.deltaTime;
            //text.text = life.ToString("n0");
        }
        else
        {
            Destroy(tempPowerUp);
            FindObjectOfType<ComboMatch>().haveTempPowerUp = false;
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        }
    }

    public void Clicked()
    {
        //FindObjectOfType<ComboMatch>().audioManager.Play("BombDeactivate");
        FindObjectOfType<ComboMatch>().audioManager.Play("PowerUp");
        FindObjectOfType<ComboMatch>().CreateTempPowerup(id);
        FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        Destroy(tempPowerUp);
    }
}
