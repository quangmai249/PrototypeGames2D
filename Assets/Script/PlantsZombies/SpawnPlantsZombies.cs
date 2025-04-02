using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlantsZombies : SingletonGeneric<SpawnPlantsZombies>
{
    [SerializeField] int numDefault = 10;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] plants;

    private GameObject _go;
    private Queue<GameObject> queueEnemies = new Queue<GameObject>();
    private Queue<GameObject> queuePlants = new Queue<GameObject>();
    private void Start()
    {
        this.SetDefaultObj(this.numDefault);
    }

    private void SetDefaultObj(int num)
    {
        for (int i = 0; i < num; i++)
            foreach (GameObject enemy in enemies)
            {
                this.CreateObj(TagName.TAG_PLANTS);
                this.CreateObj(TagName.TAG_ENEMIES);
            }
    }

    public void EnqueneAllObj()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_ENEMIES))
            this.EnqueueObj(item);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_PLANTS))
            this.EnqueueObj(item);
    }

    public void EnqueueObj(GameObject go)
    {
        go.SetActive(false);

        if (go.tag == TagName.TAG_ENEMIES)
            queueEnemies.Enqueue(go);
        else if (go.tag == TagName.TAG_PLANTS)
            queuePlants.Enqueue(go);
    }

    private void CreateObj(string tag)
    {
        if (tag == TagName.TAG_ENEMIES)
        {
            _go = Instantiate(enemies[Random.Range(0, enemies.Length)]);
            _go.transform.SetParent(this.transform);
            _go.SetActive(false);
            queueEnemies.Enqueue(_go);
        }
        else if (tag == TagName.TAG_PLANTS)
        {
            for (int i = 0; i < plants.Length; i++)
            {
                _go = Instantiate(plants[i]);
                _go.transform.SetParent(this.transform);
                _go.SetActive(false);
                queuePlants.Enqueue(_go);
            }
        }
    }

    public GameObject DequeueObj(string tag)
    {
        if (tag == TagName.TAG_ENEMIES)
        {
            if (queueEnemies.Count == 0)
                CreateObj(tag);
            return queueEnemies.Dequeue();
        }
        else if (tag == TagName.TAG_PLANTS)
        {
            if (queueEnemies.Count == 0)
                CreateObj(tag);
            return queuePlants.Dequeue();
        }
        else return null;
    }

    public GameObject DequeuePlantsWithID(int id)
    {
        foreach (GameObject item in queuePlants)
        {
            if (item.GetComponent<Plants>().ID == id && item.activeSelf == false)
                return item;
        }

        CreateObj(TagName.TAG_PLANTS);
        return DequeuePlantsWithID(id);
    }
}
