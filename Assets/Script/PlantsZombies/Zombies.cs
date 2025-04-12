using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombies : BaseEnemies
{
    [SerializeField] GameObject canvasZombies;

    private float speedDefault;
    private float healthDefault;

    private Plants plants;
    private Slider sliderHealthBar;
    private GameObject _canvasZombies;
    private RaycastHit2D raycastHit2D;

    protected override void Awake()
    {
        _canvasZombies = Instantiate(canvasZombies);
        _canvasZombies.transform.SetParent(this.transform);
        _canvasZombies.transform.position = new Vector3(this.transform.position.x, 0.3f, 0);

        base.Awake();
    }

    private void Start()
    {
        this.speedDefault = base.speed;
        this.healthDefault = base.health;

        foreach (Transform child in _canvasZombies.transform)
            this.sliderHealthBar = child.GetComponent<Slider>();

        this.sliderHealthBar.maxValue = this.healthDefault;
        this.sliderHealthBar.value = this.healthDefault;
    }

    protected override void Update()
    {
        base.Update();

        this.SetSliderHealthBar();

        if (this.gameObject.activeSelf)
            this.DetectTarget();
    }

    private void DetectTarget()
    {
        raycastHit2D = Physics2D.Raycast(this.transform.position, Vector2.left, 0.1f, LayerMask.GetMask(TagName.TAG_PLANTS));

        if (raycastHit2D.collider != null)
        {
            plants = raycastHit2D.collider.gameObject.GetComponent<Plants>();

            if (plants.IsReplace && !base.isAttacking)
            {
                this.speed = 0;
                StartCoroutine(nameof(Attack), plants);
                this.GetComponent<AudioSource>().Play();
                base.isAttacking = true;
            }
        }

        if (raycastHit2D.collider == null && base.isAttacking)
        {
            StopCoroutine(nameof(Attack));
            GetComponent<AudioSource>().Stop();
            base.speed = this.speedDefault;
            base.isAttacking = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, Vector2.left * 0.1f);
    }

    public void SetDefaultStats()
    {
        base.isAttacking = false;
        base.speed = this.speedDefault;
        base.health = this.healthDefault;

        sliderHealthBar.value = this.health;
        GetComponent<AudioSource>().Stop();
    }

    private void SetSliderHealthBar()
    {
        sliderHealthBar.value = this.health;

        if (this.health < this.healthDefault / 4)
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.red;
        else if (this.health < this.healthDefault / 2)
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.yellow;
        else
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.green;
    }

    protected virtual IEnumerator Attack(Plants plants)
    {
        while (plants.Health > 0)
        {
            plants.Health -= this.damage;
            plants.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(this.rate / 2);
            plants.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(this.rate / 2);
        }
    }
}