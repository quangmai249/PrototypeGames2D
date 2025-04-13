using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlantsZombies : SingletonGeneric<SpawnPlantsZombies>
{
    [SerializeField] int numDefault = 10;
    [SerializeField] GameObject[] rewards;
    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] plants;
    [SerializeField] GameObject[] plantsBullets;

    private GameObject _go;
    private Queue<GameObject> queuePlants = new Queue<GameObject>();
    private Queue<GameObject> queueRewards = new Queue<GameObject>();
    private Queue<GameObject> queueBullets = new Queue<GameObject>();
    private Queue<GameObject> queueEnemies = new Queue<GameObject>();
    private void Start()
    {
        this.SetDefaultObj(this.numDefault);
    }

    private void SetDefaultObj(int num)
    {
        for (int i = 0; i < num; i++)
            foreach (GameObject enemy in enemies)
                this.CreateObj(TagName.TAG_ENEMIES);

        for (int i = 0; i < num; i++)
            foreach (GameObject plant in plants)
                this.CreateObj(TagName.TAG_PLANTS);

        for (int i = 0; i < num; i++)
            foreach (GameObject plantsBullet in plantsBullets)
                this.CreateObj(TagName.TAG_BULLETS);

        for (int i = 0; i < num; i++)
            foreach (GameObject reward in rewards)
                this.CreateObj(TagName.TAG_REWARDS);
    }

    public void EnqueneAllObj()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_ENEMIES))
        {
            item.GetComponent<Zombies>().SetDefaultStats();
            this.EnqueueObj(item);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_PLANTS))
        {
            item.GetComponent<Plants>().SetDefaultStats();
            this.EnqueueObj(item);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_BULLETS))
            this.EnqueueObj(item);

        foreach (GameObject item in GameObject.FindGameObjectsWithTag(TagName.TAG_REWARDS))
        {
            item.GetComponent<RewardsPlantsandZombies>().SetDefaultStats();
            this.EnqueueObj(item);
        }
    }

    public void EnqueueObj(GameObject go)
    {
        go.SetActive(false);

        switch (go.tag)
        {
            case TagName.TAG_ENEMIES:
                queueEnemies.Enqueue(go);
                break;
            case TagName.TAG_PLANTS:
                queuePlants.Enqueue(go);
                break;
            case TagName.TAG_BULLETS:
                queueBullets.Enqueue(go);
                break;
            case TagName.TAG_REWARDS:
                queueRewards.Enqueue(go);
                break;
        }
    }

    private void CreateObj(string tag)
    {
        switch (tag)
        {
            case TagName.TAG_ENEMIES:
                {
                    _go = Instantiate(enemies[Random.Range(0, enemies.Length)]);
                    _go.transform.SetParent(this.transform);
                    _go.SetActive(false);
                    queueEnemies.Enqueue(_go);
                    break;
                }
            case TagName.TAG_PLANTS:
                {
                    for (int i = 0; i < plants.Length; i++)
                    {
                        _go = Instantiate(plants[i]);
                        _go.transform.SetParent(this.transform);
                        _go.SetActive(false);
                        queuePlants.Enqueue(_go);
                    }
                }
                break;
            case TagName.TAG_BULLETS:
                {
                    for (int i = 0; i < plantsBullets.Length; i++)
                    {
                        _go = Instantiate(plantsBullets[i]);
                        _go.transform.SetParent(this.transform);
                        _go.SetActive(false);
                        queueBullets.Enqueue(_go);
                    }
                }
                break;
            case TagName.TAG_REWARDS:
                {
                    for (int i = 0; i < rewards.Length; i++)
                    {
                        _go = Instantiate(rewards[Random.Range(0, rewards.Length)]);
                        _go.transform.SetParent(this.transform);
                        _go.SetActive(false);
                        queueRewards.Enqueue(_go);
                    }
                }
                break;
        }
    }

    public GameObject DequeueObj(string tag)
    {
        switch (tag)
        {
            case TagName.TAG_ENEMIES:
                {
                    if (queueEnemies.Count == 0)
                        CreateObj(tag);
                    return queueEnemies.Dequeue();
                }
            case TagName.TAG_PLANTS:
                {
                    if (queuePlants.Count == 0)
                        CreateObj(tag);
                    return queuePlants.Dequeue();
                }
            case TagName.TAG_BULLETS:
                {
                    if (queueBullets.Count == 0)
                        CreateObj(tag);
                    return queueBullets.Dequeue();
                }
            case TagName.TAG_REWARDS:
                {
                    if (queueRewards.Count == 0)
                        CreateObj(tag);
                    return queueRewards.Dequeue();
                }
        }
        return null;
    }

    public GameObject DequeueByID(int id, string tag)
    {
        switch (tag)
        {
            case TagName.TAG_PLANTS:
                {
                    foreach (GameObject item in queuePlants)
                    {
                        if (item.GetComponent<Plants>().ID == id && item.activeSelf == false)
                            return item;
                    }
                    CreateObj(TagName.TAG_PLANTS);
                    return DequeueByID(id, tag);
                }
            case TagName.TAG_BULLETS:
                {
                    foreach (GameObject item in queueBullets)
                    {
                        if (item.GetComponent<BulletPlants>().ID == id && item.activeSelf == false)
                            return item;
                    }
                    CreateObj(TagName.TAG_BULLETS);
                    return DequeueByID(id, tag);
                }
        }

        return null;
    }

    public float GetPricePlantsByID(int id)
    {
        foreach (GameObject item in queuePlants)
        {
            if (item.GetComponent<Plants>().ID == id)
                return item.GetComponent<Plants>().Price;
        }
        return 0;
    }
}
