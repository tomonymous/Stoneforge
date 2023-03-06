using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TokenScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject token;
    public GameObject tokenImage;
    public float life;
    public int value;
    public int x;
    public int y;

    float originalY;
    float originalX;

    public bool Initialize(int v, float l, int newX, int newY)
    {
        if (!FindObjectOfType<ComboMatch>().popups.Contains("x" + newX + "y" + newY))
        {
            this.originalY = this.transform.position.y;
            this.originalX = this.transform.position.x;
            text.text = v.ToString();
            value = v;
            life = l;
            x = newX;
            y = newY;
            FindObjectOfType<ComboMatch>().popups.Add("x" + x + "y" + y);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(originalX + ((float)Math.Sin(Time.time * 5) / 50), originalY + ((float)Math.Sin(Time.time*3)/ 20), transform.position.z); //(float)Math.Sin(Time.time*A)/B) A:Velocity, B:Distance
        if (life > 0)
        {
            life -= 1 * Time.deltaTime;
        }
        else
        {
            Destroy(token);
            FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        }
    }

    public void Clicked()
    {
        FindObjectOfType<ComboMatch>().audioManager.Play("TokenGet");
        FindObjectOfType<ComboMatch>().tokens += value;
        FindObjectOfType<ComboMatch>().totalTokens += value;
        FindObjectOfType<ComboMatch>().popups.Remove("x" + x + "y" + y);
        Destroy(token);
    }
}
