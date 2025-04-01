using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : SingletonGeneric<ButtonsController>
{
    public void ButtonPlay(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);

        AudioManager.Instance.ClickedButton();
    }

    public void ButtonSetting()
    {
        CanvasController.Instance.ActivePanel(TagName.PANEL_SETTING);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonQuiGame()
    {
        AudioManager.Instance.ClickedButton();
        Application.Quit();
    }

    public void ButtonSave()
    {
        PlayerPrefs.SetString(TagName.DATA_SETTING, JsonUtility.ToJson(SettingManager.Instance.Setting));
        PlayerPrefs.Save();

        SettingManager.Instance.SetDefaultSetting();
        CanvasController.Instance.ActivePanel(TagName.PANEL_PAUSE);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonHome()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        SceneManager.LoadScene(TagName.NAME_SCENE_HOME);
        CanvasController.Instance.ActivePanel(TagName.PANEL_HOME_SCENE);
        AudioManager.Instance.ClickedButton();
        AudioManager.Instance.PlayMusicBakground(TagName.NAME_SCENE_HOME);
    }

    public void ButtonPause()
    {
        Time.timeScale = 0;
        DOTween.timeScale = 0;
        CanvasController.Instance.ActivePanel(TagName.PANEL_PAUSE);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonResume()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonSwitchLanguage()
    {
        SettingManager.Instance.StartSwitchLanguage();
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonReplay()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CanvasController.Instance.ActivePanel(TagName.PANEL_GAMEPLAY_SCENE);
    }
}
