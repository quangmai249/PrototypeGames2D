using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);
    }

    void Update()
    {

    }
}
