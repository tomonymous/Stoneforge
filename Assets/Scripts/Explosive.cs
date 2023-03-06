using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Explosive : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject explosive;
    public GameObject explosiveImage;
    public Image number;
    public float life;
    public int x;
    public int y;
    public Sprite[] numbers;

    float originalY;
    float originalX;

    public bool Initialize(int pointX, int pointY, float l)
    {
        if (!FindObjectOfType<ComboMatch>().popups.Contains("x" + pointX + "y" + pointY))
        {
            this.originalY = this.transform.position.y;
            this.originalX = this.transform.position.x;
            //text.text = life.ToString("n0");
            int i = (int) l;
            number.sprite = numbers[i];
            life = l;
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
            int i = (int)life;
            number.sprite = numbers[i];
        }
        else
        {
            Destroy(explosive);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
            FindObjectOfType<ComboMatch>().BombAtPosition(x,y);
        }
    }

    public void Clicked()
    {
        FindObjectOfType<ComboMatch>().audioManager.Play("BombDeactivate");
        FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        Destroy(explosive);
    }
}
