using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldMinerScene : MonoBehaviour
{
    [SerializeField] GameObject instruction;

    private void Awake()
    {
        AudioManager.Instance.PlayMusicBakground(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        StartCoroutine(nameof(CoroutineInstruction));
    }

    private IEnumerator CoroutineInstruction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            instruction.SetActive(false);

            yield return new WaitForSeconds(.25f);
            instruction.SetActive(true);
        }
    }
}
