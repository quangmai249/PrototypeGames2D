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

    private PlantsZombiesScene _plantsZombiesScene;

    private void Start()
    {
        this.SetDefaultCanvasPricePlants();

        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
    }

    private void OnMouseDown()
    {
        if (_plantsZombiesScene.Gold <= 0 || _plantsZombiesScene.Gold < price || _plantsZombiesScene.IsPickup)
        {
            Debug.Log("Not enough gold or Picked up something");
            return;
        }

        _go = SpawnPlantsZombies.Instance.DequeuePlantsByID(id);
        _plantsZombiesScene.Gold -= _go.GetComponent<Plants>().Price;
        _go.transform.position = this.transform.position;
        _go.SetActive(true);

        _plantsZombiesScene.IsPickup = true;
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
