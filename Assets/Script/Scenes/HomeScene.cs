using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        DOTween.timeScale = 1;
        CanvasController.Instance.ActivePanel(TagName.PANEL_HOME_SCENE);

        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);
    }
}
