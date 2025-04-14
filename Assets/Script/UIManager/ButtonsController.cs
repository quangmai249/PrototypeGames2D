using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : SingletonGeneric<ButtonsController>
{
    private bool isIncreaseSpeed = false;

    public void ButtonPlay(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_GAMEPLAY_SCENE);

        AudioManager.Instance.ClickedButton();
    }

    public void ButtonSetting()
    {
        CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_SETTING);
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

        switch (SceneManager.GetActiveScene().name)
        {
            case "HomeScene":
                CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_HOME_SCENE);
                break;
            case "GoldMinerScene":
                CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_PAUSE);
                break;
            case "PlantsZombiesScene":
                CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_PAUSE);
                break;
            default: break;
        }

        AudioManager.Instance.ClickedButton();
    }

    public void ButtonHome()
    {
        if (SceneManager.GetActiveScene().name == TagName.NAME_SCENE_PLANTS_ZOMBIES)
            SpawnPlantsZombies.Instance.EnqueneAllObj();

        SceneManager.LoadScene(TagName.NAME_SCENE_HOME);
        AudioManager.Instance.ClickedButton();
        AudioManager.Instance.PlayMusicBakground(TagName.NAME_SCENE_HOME);
    }

    public void ButtonPause()
    {
        Time.timeScale = 0;
        DOTween.timeScale = 0;
        CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_PAUSE);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonResume()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.NAME_PANEL_GAMEPLAY_SCENE);
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonSwitchLanguage()
    {
        SettingManager.Instance.StartSwitchLanguage();
        AudioManager.Instance.ClickedButton();
    }

    public void ButtonReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonIncreaseSpeed(TextMeshProUGUI txtSpeed)
    {
        if (!isIncreaseSpeed)
        {
            Time.timeScale = 2;
            DOTween.timeScale = 2;

            this.SetTextSpeedGame(txtSpeed, Time.timeScale);

            isIncreaseSpeed = !isIncreaseSpeed;
        }
        else
        {
            Time.timeScale = 1;
            DOTween.timeScale = 1;

            this.SetTextSpeedGame(txtSpeed, Time.timeScale);

            isIncreaseSpeed = !isIncreaseSpeed;
        }
    }

    public void SetTextSpeedGame(TextMeshProUGUI txtSpeed, float timeScale)
    {
        if (SettingManager.Instance.Setting.Language == 0)
            txtSpeed.text = $"Tốc độ x{timeScale}";
        else if (SettingManager.Instance.Setting.Language == 1)
            txtSpeed.text = $"x{timeScale} speed";
    }
}
