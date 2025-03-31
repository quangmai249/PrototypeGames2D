using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : SingletonGeneric<AudioManager>
{
    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Slider")]
    [SerializeField] Slider sliderSFX;
    [SerializeField] Slider sliderMusic;

    [Header("AudioSource")]
    [SerializeField] AudioSource audioSFXSource;
    [SerializeField] AudioSource audioMusicSource;

    [Header("AudioClips")]
    [SerializeField] AudioClip clipClickButton;
    [SerializeField] AudioClip clipCoin;
    [SerializeField] AudioClip clipPing;
    [SerializeField] AudioClip clipBomb;
    [SerializeField] AudioClip[] clipsBG;

    void Start()
    {
        sliderSFX.onValueChanged.AddListener(ChangeValueSFX);
        sliderMusic.onValueChanged.AddListener(ChangeValueMusic);

        this.PlayMusicBakground(TagName.NAME_SCENE_HOME);
    }

    private void ChangeValueMusic(float value)
    {
        audioMixer.SetFloat(TagName.NAME_MIXER_MUSIC, Mathf.Log10(value) * 20);
        SettingManager.Instance.Setting.MusicVol = value;
    }

    private void ChangeValueSFX(float value)
    {
        audioMixer.SetFloat(TagName.NAME_MIXER_SFX, Mathf.Log10(value) * 20);
        SettingManager.Instance.Setting.SfxVol = value;
    }

    public void DefaultSettingVolume()
    {
        sliderSFX.value = SettingManager.Instance.Setting.SfxVol;
        sliderMusic.value = SettingManager.Instance.Setting.MusicVol;

        audioMixer.SetFloat(TagName.NAME_MIXER_MUSIC, Mathf.Log10(SettingManager.Instance.Setting.MusicVol) * 20);
        audioMixer.SetFloat(TagName.NAME_MIXER_SFX, Mathf.Log10(SettingManager.Instance.Setting.SfxVol) * 20);
    }

    public void PlayMusicBakground(string name)
    {
        if (Searching.ByName(name, clipsBG) == null)
            return;

        audioMusicSource.clip = Searching.ByName<AudioClip>(name, clipsBG);
        audioMusicSource.loop = true;
        audioMusicSource.Play();
    }

    public void ClickedButton()
    {
        audioSFXSource.PlayOneShot(clipClickButton);
    }

    public void CoinSound()
    {
        audioSFXSource.PlayOneShot(clipCoin);
    }

    public void PingSound()
    {
        audioSFXSource.PlayOneShot(clipPing);
    }

    public void BombSound()
    {
        audioSFXSource.PlayOneShot(clipBomb);
    }

    public void AudioClipSound(AudioClip clip)
    {
        audioSFXSource.PlayOneShot(clip);
    }
}
