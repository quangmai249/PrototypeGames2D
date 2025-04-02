using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : SingletonGeneric<CanvasController>
{
    private GameObject _panelPause;
    private GameObject _panelSetting;
    private GameObject _panelHomeScene;
    private GameObject _panelGameplayScene;
    private GameObject _panelEndGame;

    private List<GameObject> _panels = new List<GameObject>();

    private void Start()
    {
        this.SetPanelDefault();
        this.ActivePanel(TagName.PANEL_HOME_SCENE);
    }

    private void SetPanelDefault()
    {
        _panelPause = Searching.GameObjectByName(TagName.PANEL_PAUSE, TagName.TAG_PANEL);
        _panelSetting = Searching.GameObjectByName(TagName.PANEL_SETTING, TagName.TAG_PANEL);
        _panelHomeScene = Searching.GameObjectByName(TagName.PANEL_HOME_SCENE, TagName.TAG_PANEL);
        _panelGameplayScene = Searching.GameObjectByName(TagName.PANEL_GAMEPLAY_SCENE, TagName.TAG_PANEL);
        _panelEndGame = Searching.GameObjectByName(TagName.PANEL_END_GAME, TagName.TAG_PANEL);

        _panels.Add(_panelPause);
        _panels.Add(_panelSetting);
        _panels.Add(_panelHomeScene);
        _panels.Add(_panelGameplayScene);
        _panels.Add(_panelEndGame);
    }

    public void ActivePanel(string name)
    {
        foreach (GameObject item in _panels)
        {
            if (item.name == name)
                item.SetActive(true);
            else if (item.activeSelf == true)
                item.SetActive(false);
        }
    }

    public void SetTextPanelEndGame(bool isWin)
    {
        Time.timeScale = 0;
        DOTween.timeScale = 0;

        this.ActivePanel(TagName.PANEL_END_GAME);

        foreach (Transform item in _panelEndGame.transform)
        {
            if (isWin)
                item.GetComponent<TextMeshProUGUI>().text = "YOU WIN!";
            else
                item.GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
            return;
        }
    }
}
