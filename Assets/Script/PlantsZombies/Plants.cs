using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    [SerializeField] int id;

    public int ID
    {
        get => this.id;
        set => this.id = value;
    }

    private void OnMouseDown()
    {
        SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
    }
}
