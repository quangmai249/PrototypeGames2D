using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingPlants : MonoBehaviour
{
    [SerializeField] int id;

    private GameObject _go;
    private void OnMouseDown()
    {
        _go = SpawnPlantsZombies.Instance.DequeuePlantsWithID(id);
        _go.transform.position = Vector3.zero;
        _go.SetActive(true);
    }
}
