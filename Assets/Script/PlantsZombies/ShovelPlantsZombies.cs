using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShovelPlantsZombies : MonoBehaviour
{
    [SerializeField] bool isClicked = false;

    private Vector3 posDefault;
    private Vector3 pos;
    private Collider2D plant;
    private PlantsZombiesScene _plantsZombiesScene;

    private void Start()
    {
        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
        posDefault = this.transform.position;
    }

    private void Update()
    {
        if (isClicked)
            this.transform.position = posMouse();
        else
            this.transform.position = posDefault;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isClicked = false;
            _plantsZombiesScene.IsPickup = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && plant != null)
        {
            plant.GetComponent<Plants>().SetDefaultStats();
            plant.GetComponent<Plants>().SetDefaultConstruction();
            SpawnPlantsZombies.Instance.EnqueueObj(plant.gameObject);
            isClicked = false;
            _plantsZombiesScene.IsPickup = false;
        }
    }

    private void OnMouseDown()
    {
        isClicked = true;
        _plantsZombiesScene.IsPickup = true;
    }

    private Vector3 posMouse()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isClicked && collision.CompareTag(TagName.TAG_PLANTS))
            plant = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isClicked && collision.CompareTag(TagName.TAG_PLANTS))
            plant = null;
    }

    public bool IsClicked
    {
        get => this.isClicked;
        set => this.isClicked = value;
    }
}
