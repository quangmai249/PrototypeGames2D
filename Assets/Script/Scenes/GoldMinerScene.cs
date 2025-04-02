using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoldMinerScene : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int numBombs = 3;
    [SerializeField] int maxScore = 1000;
    [SerializeField] float maxTime = 60;
    [SerializeField] float defaultMaxTime;

    [Header("Slider")]
    [SerializeField] Slider sliderTime;
    [SerializeField] Image sliderFill;

    [Header("UI")]
    [SerializeField] GameObject tap;
    [SerializeField] TextMeshProUGUI txtNumBombs;
    [SerializeField] TextMeshProUGUI txtTarget;
    [SerializeField] ParticleSystem parExplosion;

    private PlayerGoldMiner playerGoldMiner;
    private HomeScene homeScene;
    private GameObject panelEndGame;
    private void Awake()
    {
        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);

        playerGoldMiner = GameObject.FindGameObjectWithTag(TagName.TAG_PLAYER).GetComponent<PlayerGoldMiner>();
        homeScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<HomeScene>();
        parExplosion.Stop();
    }

    private void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);

        tap.transform.DOScale(new Vector3(.85f, .85f, .85f), 2f)
          .SetLoops(-1, LoopType.Yoyo)
          .SetEase(Ease.InOutSine);

        defaultMaxTime = maxTime;
        sliderTime.value = maxTime;

        txtNumBombs.text = numBombs.ToString();
        txtTarget.text = $"Target : {maxScore}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            this.ButtonBomb();

        if (playerGoldMiner.Score >= maxScore)
            this.EndGame(true);
        else if (maxTime <= 0)
            this.EndGame(false);

        maxTime -= Time.deltaTime;

        sliderTime.value = maxTime;
        ChangeSliderColor();
    }

    private void ButtonBomb()
    {
        if (Time.timeScale == 0)
            return;

        if (numBombs > 0 && playerGoldMiner.GoItem != null)
        {
            parExplosion.transform.position
                = new Vector3(playerGoldMiner.GoItem.transform.position.x, playerGoldMiner.GoItem.transform.position.y, -1);
            parExplosion.Play();

            Destroy(playerGoldMiner.GoItem);
            numBombs--;
            txtNumBombs.text = numBombs.ToString();

            AudioManager.Instance.BombSound();
        }
    }

    private void EndGame(bool isWin)
    {
        Time.timeScale = 0;
        DOTween.timeScale = 0;
        CanvasController.Instance.ActivePanel(TagName.PANEL_END_GAME);
        CanvasController.Instance.SetTextPanelEndGame(isWin);
    }

    private void ChangeSliderColor()
    {
        if (sliderTime.value > defaultMaxTime / 2)
            sliderFill.color = Color.green;
        else if (sliderTime.value > defaultMaxTime / 3)
            sliderFill.color = Color.yellow;
        else if (sliderTime.value > 0)
            sliderFill.color = Color.red;
        else
            sliderFill.color = Color.black;
    }
}
