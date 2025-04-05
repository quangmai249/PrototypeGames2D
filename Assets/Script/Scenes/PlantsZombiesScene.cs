using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlantsZombiesScene : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] int gold = 500;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float maxTimeplay = 120;

    [Header("Spawn")]
    [SerializeField] float timeSpawn = 5;
    [SerializeField] Transform[] posSpawn;

    [Header("UI")]
    [SerializeField] Slider sliderTimePlay;
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI txtHealth;

    private float defaultMaxTimeplay;
    private float defaultMaxHealth;
    private float defaultTimeSpawn;
    private GameObject _enemies;

    private void Awake()
    {
        SpawnPlantsZombies.Instance.EnqueneAllObj();
    }

    private void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);

        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);

        defaultTimeSpawn = this.timeSpawn;
        defaultMaxHealth = this.maxHealth;
        defaultMaxTimeplay = this.maxTimeplay;

        sliderHealth.maxValue = defaultMaxHealth;
        sliderHealth.value = defaultMaxHealth;

        sliderTimePlay.maxValue = defaultMaxTimeplay;
        sliderTimePlay.value = defaultMaxTimeplay;

        txtHealth.text = sliderHealth.value.ToString() + "/" + defaultMaxHealth;
    }

    private void Update()
    {
        maxTimeplay -= Time.deltaTime;
        sliderTimePlay.value = maxTimeplay;

        timeSpawn -= Time.deltaTime;
        if (timeSpawn <= 0)
        {
            _enemies = SpawnPlantsZombies.Instance.DequeueObj(TagName.TAG_ENEMIES);
            _enemies.transform.position = posSpawn[Random.Range(0, posSpawn.Length)].position;
            _enemies.SetActive(true);
            timeSpawn = defaultTimeSpawn;
        }

        CheckWinGame();
    }

    private void CheckWinGame()
    {
        if (maxTimeplay <= 0)
            CanvasController.Instance.SetTextPanelEndGame(true);
        else if (maxHealth <= 0)
            CanvasController.Instance.SetTextPanelEndGame(false);
    }

    public void ChangeValueHealth(float value)
    {
        maxHealth -= value;
        sliderHealth.value = maxHealth;
        txtHealth.text = maxHealth.ToString() + "/" + defaultMaxHealth;
    }

    public int Gold
    {
        get => this.gold;
        set => this.gold = value;
    }
}
