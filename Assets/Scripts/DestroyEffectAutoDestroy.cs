using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectAutoDestroy : MonoBehaviour
{
    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rt)
        {
            if (rt.childCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
