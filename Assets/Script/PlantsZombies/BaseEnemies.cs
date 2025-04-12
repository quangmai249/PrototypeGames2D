using System.Collections;
using UnityEngine;

public abstract class BaseEnemies : MonoBehaviour
{
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float health = 100;
    [SerializeField] protected float damage = 10;
    [SerializeField] protected float rate = 1;
    [SerializeField] protected float detectDistance = 0.1f;

    private Transform _rewards;
    protected virtual void Awake()
    {

    }

    protected virtual void Update()
    {
        if (this.health <= 0)
        {
            this.GetComponent<Zombies>().SetDefaultStats();
            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);

            _rewards = SpawnPlantsZombies.Instance.DequeueObj(TagName.TAG_REWARDS).transform;
            _rewards.position = this.transform.position;
            _rewards.gameObject.SetActive(true);

            return;
        }

        this.transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public float Health
    {
        get => this.health;
        set => this.health = value;
    }
}
