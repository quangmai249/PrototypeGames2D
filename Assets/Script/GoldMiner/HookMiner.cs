using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMiner : MonoBehaviour
{
    [SerializeField] GameObject itemReach;
    private PlayerGoldMiner playerGoldMiner;
    private void Awake()
    {
        playerGoldMiner = GameObject.FindGameObjectWithTag(TagName.TAG_PLAYER).GetComponent<PlayerGoldMiner>();
    }

    private void Update()
    {
        if (itemReach != null)
        {
            itemReach.transform.position = this.transform.position;

            if (playerGoldMiner.GoItem == null)
                playerGoldMiner.GoItem = itemReach.gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_DIAMOND))
        {
            if (collision.GetComponent<ItemGoldMinerMoving>() != null)
                AudioManager.Instance.AudioClipSound(collision.GetComponent<ItemGoldMinerMoving>().Clip);
            else
                AudioManager.Instance.PingSound();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_DIAMOND))
            itemReach = collision.gameObject;
    }
}
