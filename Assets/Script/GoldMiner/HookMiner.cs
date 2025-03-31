using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMiner : MonoBehaviour
{
    private PlayerGoldMiner playerGoldMiner;
    private void Awake()
    {
        playerGoldMiner = GameObject.FindGameObjectWithTag(TagName.TAG_PLAYER).GetComponent<PlayerGoldMiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PingSound();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_DIAMOND))
        {
            collision.transform.position = this.transform.position;

            if (playerGoldMiner.GoItem == null)
                playerGoldMiner.GoItem = collision.gameObject;
        }
    }
}
