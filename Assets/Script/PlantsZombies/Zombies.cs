using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float health = 100;
    [SerializeField] float damage = 10;

    private Vector3 defaultPos;

    private void Update()
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
