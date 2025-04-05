using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosShoppingDefault : MonoBehaviour
{
    [SerializeField] bool isFull;

    private void Start()
    {
        isFull = false;
    }

    private void Update()
    {
        isFull = IsFullPosShoppingDefault();
    }

    private bool IsFullPosShoppingDefault()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_PLANTS))
        {
            if (item.transform.position == this.gameObject.transform.position)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsFull
    {
        get => isFull;
        set => isFull = value;
    }
}
