using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBase : MonoBehaviour
{
    [SerializeField] GameObject parExplosion;
    private GameObject _parExplosion;
    private PlantsZombiesScene _plantsZombiesScene;
    private void Start()
    {
        _parExplosion = Instantiate(parExplosion);
        _parExplosion.transform.SetParent(this.gameObject.transform);
        _parExplosion.transform.position = this.transform.position;

        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag(TagName.TAG_ENEMIES))
        {
            SpawnPlantsZombies.Instance.EnqueueObj(collision.gameObject);
            AudioManager.Instance.BombSound();

            _parExplosion.GetComponent<ParticleSystem>().Play();
            _plantsZombiesScene.ChangeValueHealth(collision.GetComponent<Zombies>().Damage);
        }
    }
}
