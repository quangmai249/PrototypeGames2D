using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPoints : MonoBehaviour
{
    [SerializeField] bool isFull;

    private void Start()
    {
        isFull = false;
    }

    public bool IsFull
    {
        get => isFull;
        set => isFull = value;
    }
}
