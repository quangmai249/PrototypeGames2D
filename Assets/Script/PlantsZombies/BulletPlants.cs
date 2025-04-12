using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlants : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;

    private Plants _plant;

    private void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_ENEMIES))
        {
            collision.GetComponent<Zombies>().Health -= _plant.Damage;
            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
        }
    }

    public Plants Plant
    {
        get => _plant;
        set => _plant = value;
    }
}
