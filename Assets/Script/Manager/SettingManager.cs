using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingManager : SingletonGeneric<SettingManager>
{
    [SerializeField] TMP_Dropdown dropDownMode, dropDownResolution;

    private Locale en, vie;
    private Setting setting;
    void Start()
    {
        en = LocalizationSettings.AvailableLocales.Locales[0];
        vie = LocalizationSettings.AvailableLocales.Locales[1];

        dropDownMode.onValueChanged.AddListener(StartSwitchMode);

        this.SetDefaultSetting();
    }

    public void StartSwitchLanguage()
    {
        if (LocalizationSettings.SelectedLocale == en)
        {
            LocalizationSettings.SelectedLocale = vie;
            setting.Language = 0;
        }
        else
        {
            LocalizationSettings.SelectedLocale = en;
            setting.Language = 1;
        }
    }

    private void StartSwitchMode(int index)
    {
        if (index == 0)
        {
            Screen.fullScreen = false;
            setting.ModeScreen = 1;
        }
        else
        {
            Screen.fullScreen = true;
            setting.ModeScreen = 0;
        }
    }

    public void SetDefaultSetting()
    {
        setting
            = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString(TagName.DATA_SETTING, JsonUtility.ToJson(new Setting())));

        Debug.Log(setting.Language + "\t" + setting.ModeScreen + "\t" + setting.MusicVol + "\t" + setting.SfxVol);

        //languages
        if (setting.Language == 0)
            LocalizationSettings.SelectedLocale = vie;
        else
            LocalizationSettings.SelectedLocale = en;

        //modeScene
        if (setting.ModeScreen == 0)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;

        //volume
        AudioManager.Instance.DefaultSettingVolume();
    }

    public Setting Setting
    {
        get => this.setting;
        set => this.setting = value;
    }
}
