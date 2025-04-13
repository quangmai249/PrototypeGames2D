using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPlantsandZombies : MonoBehaviour
{
    [SerializeField] bool isStartSpawn = false;
    [SerializeField] float goldReward = 100;

    private Transform _posTotalGold;
    private PlantsZombiesScene _plantsZombiesScene;
    private void Start()
    {
        _posTotalGold = GameObject.FindGameObjectWithTag(TagName.TAG_POS_TOTAL_GOLD).transform;
        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();
    }

    private void Update()
    {
        if (gameObject.activeSelf && this.transform.position == _posTotalGold.transform.position)
        {
            SetDefaultStats();
            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
            _plantsZombiesScene.Gold += goldReward;
            AudioManager.Instance.CoinSound();
            return;
        }

        if (isStartSpawn)
        {
            isStartSpawn = false;
            StartCoroutine(nameof(CoroutineCollectReward));
        }
    }

    private void OnMouseDown()
    {
        this.gameObject.transform.DOMove(_posTotalGold.transform.position, 1f);
    }

    private IEnumerator CoroutineCollectReward()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.transform.DOMove(_posTotalGold.transform.position, 1f);
    }

    public void SetDefaultStats()
    {
        DOTween.Kill(this.gameObject.transform);
        isStartSpawn = false;
    }

    public bool IsStartSpawn
    {
        get => this.isStartSpawn;
        set => this.isStartSpawn = value;
    }
}
