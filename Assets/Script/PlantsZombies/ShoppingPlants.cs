using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShoppingPlants : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float price;
    [SerializeField] GameObject canvasPricePlants;
    [SerializeField] TextMeshProUGUI txtPrice;

    private GameObject _go;
    private GameObject _canvasPricePlants;

    private PosShoppingDefault _posShoppingDefault;
    private PlantsZombiesScene _plantsZombiesScene;

    private void Start()
    {
        this.SetDefaultCanvasPricePlants();

        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
        _posShoppingDefault = GameObject.FindGameObjectWithTag(TagName.TAG_POS_SHOPPING_DEFAULT).GetComponent<PosShoppingDefault>();
    }

    private void OnMouseDown()
    {
        if (_posShoppingDefault.IsFull)
        {
            Debug.Log("Position is full");
            return;
        }

        if (_plantsZombiesScene.Gold <= 0 || _plantsZombiesScene.Gold < price)
        {
            Debug.Log("Not enough gold");
            return;
        }

        _go = SpawnPlantsZombies.Instance.DequeuePlantsByID(id);
        _plantsZombiesScene.Gold -= _go.GetComponent<Plants>().Price;
        _go.transform.position = _posShoppingDefault.gameObject.transform.position;
        _go.SetActive(true);
    }

    private void SetDefaultCanvasPricePlants()
    {
        _canvasPricePlants = Instantiate(canvasPricePlants);
        _canvasPricePlants.transform.SetParent(this.transform);
        _canvasPricePlants.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f);

        foreach (Transform child in _canvasPricePlants.transform)
        {
            txtPrice = child.GetComponent<TextMeshProUGUI>();
            price = SpawnPlantsZombies.Instance.GetPricePlantsByID(id);
            txtPrice.text = price.ToString();
            return;
        }
    }
}
