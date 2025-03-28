using UnityEngine;
public class Setting
{
    [SerializeField] private int language;
    [SerializeField] private int modeScreen;

    [SerializeField] private float sfxVol;
    [SerializeField] private float musicVol;

    public Setting()
    {
        language = 0;
        modeScreen = 0;

        sfxVol = 0.5f;
        musicVol = 0.5f;
    }

    public Setting(int language, int modeScreen, float sfxVol, float musicVol)
    {
        this.Language = language;
        this.ModeScreen = modeScreen;
        this.SfxVol = sfxVol;
        this.MusicVol = musicVol;
    }

    public int Language { get => language; set => language = value; }
    public int ModeScreen { get => modeScreen; set => modeScreen = value; }
    public float SfxVol { get => sfxVol; set => sfxVol = value; }
    public float MusicVol { get => musicVol; set => musicVol = value; }
}
