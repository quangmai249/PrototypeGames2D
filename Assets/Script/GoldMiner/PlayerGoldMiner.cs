using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGoldMiner : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] bool isDraw = false;
    [SerializeField] bool isReach = false;
    [SerializeField] float maxY = 7f;
    [SerializeField] float speedIncrease = 5f;
    [SerializeField] int score = 0;

    [Header("Line")]
    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject hook;
    [SerializeField] GameObject tool;

    private float distance;
    private Vector3 defaultHookPos;
    private GameObject goItem;

    private void Start()
    {
        lineRenderer.textureScale = new Vector2(1, 0);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, tool.transform.position);

        defaultHookPos = hook.transform.position;

        goItem = null;

        txtScore.text = score.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!isDraw && !isReach)
            {
                isDraw = true;
                isReach = true;
            }
        }

        this.StartDrawLine();
    }

    public void StartDrawLine()
    {
        distance = Vector2.Distance(defaultHookPos, hook.transform.position);

        if (isDraw && distance <= maxY)
            hook.transform.Translate(-speedIncrease * new Vector3(0, 1, 0) * Time.deltaTime);
        else
            isDraw = false;

        if (goItem != null)
            isDraw = false;

        if (!isDraw && isReach)
            this.CheckReachItem();

        lineRenderer.SetPosition(1, hook.transform.position);
    }

    private void CheckReachItem()
    {
        if (goItem != null)
        {
            hook.transform.Translate(goItem.GetComponent<ItemGoldMinerStats>().SpeedReach * new Vector3(0, 1, 0) * Time.deltaTime);

            if (distance <= 0.5f && goItem != null)
            {
                score += goItem.GetComponent<ItemGoldMinerStats>().Score;
                Destroy(goItem);
                txtScore.text = score.ToString();

                AudioManager.Instance.CoinSound();
            }
        }
        else
        {
            hook.transform.Translate(speedIncrease * 2 * new Vector3(0, 1, 0) * Time.deltaTime);

            if (distance <= 0.5f)
                isReach = false;
        }
    }

    public bool IsReach
    {
        get => this.isReach;
        set => this.isReach = value;
    }

    public GameObject GoItem
    {
        get => this.goItem;
        set => this.goItem = value;
    }

    public int Score
    {
        get => this.score;
    }
}
