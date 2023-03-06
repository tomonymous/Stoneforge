using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int value;
    public Point index;
    public int bombStatus = 0;
    public bool justExploded = false;
    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public RectTransform rect;
    public int infectionPhase;

    bool updating;
    Image img;
    public Sprite infection1;
    public Sprite infection2;
    public GameObject infection;

    public void Initialize(int v, Point p, Sprite piece)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        value = v;
        SetIndex(p);
        img.sprite = piece;
        infection.SetActive(false);
        infectionPhase = 0;
    }
    public void Initialize(int v, Point p, Sprite piece, int phase)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        value = v;
        SetIndex(p);
        img.sprite = piece;
        infectionPhase = phase;
        if(phase > 0)
        {
            infection.SetActive(true);
        }
        else
        {
            infection.SetActive(false);
        }
    }

    public void SetIndex(Point p)
    {
        index = p;
        ResetPosition();
        UpdateName();
    }

    public Point GetIndex()
    {
        return index;
    }

    public void ResetPosition()
    {
        pos = new Vector2(32 + (64 * index.x), -32 - (64 * index.y));   
    }

    public void UpdateSprite(Sprite newSkin)
    {
        img.sprite = newSkin;
    }

    public bool UpdatePiece()
    {
        if(Vector2.Distance(rect.anchoredPosition, pos) > 1) // He used Vector3 here but I think that wasn't necessary? !!!Change if that was actually for a reason!!!
        {
            MovePositionTo(pos);
            updating = true;
            return true;
        }
        else
        {
            rect.anchoredPosition = pos;
            updating = false;
            return false;
        }
        //return false if it is not moving
    }
    public void MovePosition(Vector2 move)
    {
        rect.anchoredPosition = move * Time.deltaTime * 16f;
    }

    public void MovePositionTo(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 16f);
    }


    void UpdateName()
    {
        transform.name = "Node {" + index.x + ", " + index.y + "]";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (updating) return;
        MovePieces.instance.MovePiece(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(bombStatus > 0)
        {
            bombStatus = 0;
        }
        MovePieces.instance.DropPiece();

    }
    public int infect()
    {
        if(infectionPhase <= 0)
        {
            infectionPhase = 5;
            infection.SetActive(true);
            RectTransform infectionRect = infection.GetComponent<RectTransform>();
            infectionRect.Rotate(Vector3.forward * Random.Range(-90, 250));
            return infectionPhase;
        }
        else
        {

            infectionPhase--;
            Image infectImage = infection.GetComponent<Image>();
            infectImage.sprite = infection1;
            if (infectionPhase < 3)
            {
                infectImage.sprite = infection2;
            }
            //if(infectionPhase == 0)
            //{
            //    infection.SetActive(false);
            //    if(value == FindObjectOfType<ComboMatch>().pieces.Length - 1)
            //    {

            //    }
            //    else if(value == 0)
            //    {
            //        value = FindObjectOfType<ComboMatch>().pieces.Length - 1;
            //        img = GetComponent<Image>();
            //        img.sprite = FindObjectOfType<ComboMatch>().pieces[value];
            //    }
            //    else
            //    {
            //        value--;
            //        img = GetComponent<Image>();
            //        img.sprite = FindObjectOfType<ComboMatch>().pieces[value];
            //    }
            //    return infectionPhase;

            //}
            return infectionPhase;
        }

    }
}
