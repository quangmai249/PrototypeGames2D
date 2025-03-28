using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : SingletonGeneric<ButtonsController>
{
    public void ButtonPlay()
    {
        SceneManager.LoadScene(TagName.NAME_SCENE_GAMEPLAY);
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);
    }

    public void ButtonSetting()
    {
        CanvasController.Instance.ActivePanel(TagName.PANEL_SETTING);
    }

    public void ButtonQuiGame()
    {
        Application.Quit();
    }

    public void ButtonSave()
    {
        CanvasController.Instance.ActivePanel(TagName.PANEL_PAUSE);
    }

    public void ButtonPause()
    {
        Time.timeScale = 0;
        DOTween.timeScale = 0;
        CanvasController.Instance.ActivePanel(TagName.PANEL_PAUSE);
    }

    public void ButtonResume()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);
    }
}
