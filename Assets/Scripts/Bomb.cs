using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bomb : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    public int id;
    public int quantity;
    public int listNumber = -1;
    public bool isArmed;
    public bool activated;
    public bool paidFor = false;
    public string bombName;
    public string description;
    public string cost;
    public Image img;

    //public void Initialize(int v, Sprite piece, string n, string d, int c)
    //{
    //    cost = c / 1000 + "K";
    //    img = GetComponent<Image>();
    //    activated = true;
    //    bombName = n;
    //    description = d;
    //    id = v;
    //    quantity = 1;
    //    UpdateName();
    //    img.sprite = piece;
    //}

    //void UpdateName()
    //{
    //    transform.name = "Bomb {" + bombName + "]";
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (!isArmed)
    //    {
    //        isArmed = Powerups.instance.ActivateBomb(this);
    //    }
    //    else
    //    {
    //        isArmed = !(Powerups.instance.deActivateBomb(this));
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    //MovePieces.instance.DropPiece();

    //}
}
