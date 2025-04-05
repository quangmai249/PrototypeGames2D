using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingPlants : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int price;

    private GameObject _go;
    private PosShoppingDefault _posShoppingDefault;
    private PlantsZombiesScene _plantsZombiesScene;

    private void Start()
    {
        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
        _posShoppingDefault = GameObject.FindGameObjectWithTag(TagName.TAG_POS_SHOPPING_DEFAULT).GetComponent<PosShoppingDefault>();
    }

    private void OnMouseDown()
    {
        if (_plantsZombiesScene.Gold < price || _posShoppingDefault.IsFull)
        {
            Debug.Log("Not enough gold or position is full");
            return;
        }

        _go = SpawnPlantsZombies.Instance.DequeuePlantsWithID(id);
        _go.transform.position = _posShoppingDefault.gameObject.transform.position;
        _go.SetActive(true);
    }
}
