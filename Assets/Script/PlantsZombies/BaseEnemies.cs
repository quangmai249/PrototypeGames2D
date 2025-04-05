using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEnemies : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float health = 100;
    [SerializeField] protected float damage = 10;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public float Damage
    {
        get => this.damage;
        set => this.damage = value;
    }

    public float Health
    {
        get => this.health;
        set => this.health = value;
    }
}
