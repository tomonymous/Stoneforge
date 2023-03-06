using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory Found!");
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 3;

    public List<Item> items = new List<Item>();

    public void Add (Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("I'm full");
            return;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

}
