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
    [SerializeField] bool isStartSpawn = false;
    [SerializeField] bool isPickup = false;
    [SerializeField] float gold = 500;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float maxTimeplay = 120;

    [Header("Spawn")]
    [SerializeField] float timeSpawn = 5;
    [SerializeField] Transform[] posSpawn;
    [SerializeField] AudioSource audioSource;

    [Header("UI")]
    [SerializeField] Slider sliderTimePlay;
    [SerializeField] Slider sliderHealth;
    [SerializeField] TextMeshProUGUI txtHealth;
    [SerializeField] TextMeshProUGUI txtGold;

    private float defaultMaxTimeplay;
    private float defaultMaxHealth;
    private float defaultTimeSpawn;
    private GameObject _enemies;

    private void Awake()
    {
        ButtonsController.Instance
            .SetTextSpeedGame(GameObject.FindGameObjectWithTag(TagName.TAG_TXT_SPEED_GAME).GetComponent<TextMeshProUGUI>(), Time.timeScale);
        SpawnPlantsZombies.Instance.EnqueneAllObj();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_GAMEPLAY_SCENE);
        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);

        defaultTimeSpawn = this.timeSpawn;
        defaultMaxHealth = this.maxHealth;
        defaultMaxTimeplay = this.maxTimeplay;

        sliderHealth.maxValue = defaultMaxHealth;
        sliderHealth.value = defaultMaxHealth;

        sliderTimePlay.maxValue = defaultMaxTimeplay;
        sliderTimePlay.value = defaultMaxTimeplay;
        sliderTimePlay.gameObject.SetActive(false);

        txtGold.text = gold.ToString() + "$";
        txtHealth.text = sliderHealth.value.ToString() + "/" + defaultMaxHealth;
    }

    private void Update()
    {
        timeSpawn -= Time.deltaTime;

        if (isStartSpawn)
        {
            maxTimeplay -= Time.deltaTime;
            sliderTimePlay.value = maxTimeplay;
        }

        if (!isStartSpawn && timeSpawn <= 0)
        {
            isStartSpawn = true;
            sliderTimePlay.gameObject.SetActive(true);
            audioSource.Play();
        }

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

    public float Gold
    {
        get => this.gold;
        set
        {
            this.gold = value;
            this.txtGold.text = this.gold.ToString() + "$";
        }
    }

    public bool IsPickup
    {
        get => this.isPickup;
        set => this.isPickup = value;
    }
}
