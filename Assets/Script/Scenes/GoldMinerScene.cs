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
    [SerializeField] Slider sliderTime;

    [Header("UI")]
    [SerializeField] GameObject tap;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject panelEndGame;
    [SerializeField] TextMeshProUGUI txtEndGame;
    [SerializeField] TextMeshProUGUI txtNumBombs;
    [SerializeField] TextMeshProUGUI txtTarget;
    [SerializeField] ParticleSystem parExplosion;

    private PlayerGoldMiner playerGoldMiner;
    private void Awake()
    {
        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);
        playerGoldMiner = GameObject.FindGameObjectWithTag(TagName.TAG_PLAYER).GetComponent<PlayerGoldMiner>();

        panelEndGame.SetActive(false);
        parExplosion.Stop();
    }

    private void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;

        tap.transform.DOScale(new Vector3(.5f, .5f, .5f), 2f)
          .SetLoops(-1, LoopType.Yoyo)
          .SetEase(Ease.InOutSine);

        instruction.transform.DOScale(new Vector3(0, 0, 0), 5);

        sliderTime.value = maxTime;

        txtNumBombs.text = numBombs.ToString();
        txtTarget.text = $"Target : {maxScore}";
    }

    private void Update()
    {
        if (playerGoldMiner.Score >= maxScore || maxTime <= 0)
        {
            if (playerGoldMiner.Score >= maxScore)
                txtEndGame.text = "Win Game";
            else
                txtEndGame.text = "Lose Game";

            Time.timeScale = 0;
            DOTween.timeScale = 0;

            panelEndGame.SetActive(true);
        }

        maxTime -= Time.deltaTime;
        sliderTime.value = maxTime;
    }

    public void ButtonBomb()
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

    public void ButtonReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonHome()
    {
        SceneManager.LoadScene(TagName.NAME_SCENE_HOME);
        CanvasController.Instance.ActivePanel(TagName.PANEL_HOME_SCENE);
    }
}
