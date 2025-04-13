using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlants : MonoBehaviour
{
    [SerializeField] int id = 0;
    [SerializeField] float speed = 0.5f;

    private Plants _plant;
    private Zombies _zombies;
    private void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_ENEMIES))
        {
            _zombies = collision.GetComponent<Zombies>();
            _zombies.Health -= _plant.Damage;

            _zombies.ParAttacked.transform.position = _zombies.transform.position;
            _zombies.ParAttacked.Play();

            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
        }
    }

    public Plants Plant
    {
        get => _plant;
        set => _plant = value;
    }

    public int ID
    {
        get => this.id;
    }

    public float Speed
    {
        get => this.speed;
        set => this.speed = value;
    }
}
