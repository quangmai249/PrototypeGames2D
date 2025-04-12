using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPlantsandZombies : MonoBehaviour
{
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
            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
            _plantsZombiesScene.Gold += goldReward;
        }
    }

    private void OnMouseDown()
    {
        this.gameObject.transform.DOMove(_posTotalGold.transform.position, 2f);
    }

    public void SetDefaultStats()
    {
        DOTween.Kill(this.gameObject.transform);
    }
}
