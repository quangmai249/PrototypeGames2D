using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGoldMinerMoving : MonoBehaviour
{
    [SerializeField] bool isFlip;
    [SerializeField] float speed = 1;
    [SerializeField] float maxDistance = 5;
    [SerializeField] Vector3 direct;
    [SerializeField] AudioClip clip;

    private float distance;
    private Vector3 defaultPos;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        defaultPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        distance = Vector2.Distance(defaultPos, this.transform.position);

        if (isFlip)
        {
            this.transform.Translate(speed * direct * Time.deltaTime);

            if (distance >= maxDistance)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                isFlip = false;
            }
        }
        else
        {
            this.transform.Translate(-speed * direct * Time.deltaTime);

            if (distance <= 0.5f)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
                isFlip = true;
            }
        }
    }

    public AudioClip Clip
    {
        get => this.clip;
    }
}
