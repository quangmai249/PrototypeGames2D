using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolMiner : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] bool isReturn = false;
    [SerializeField] float angle;
    [SerializeField] float speedRotation = 1;
    [SerializeField] float maxAngle = 85;

    private PlayerGoldMiner playerGoldMiner;

    private void Awake()
    {
        playerGoldMiner = GameObject.FindGameObjectWithTag(TagName.TAG_PLAYER).GetComponent<PlayerGoldMiner>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (playerGoldMiner.IsReach == false)
            {
                isReturn = !isReturn;
                yield return new WaitForSeconds(4);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Update()
    {
        if (playerGoldMiner.IsReach == true)
            return;

        if (isReturn)
            angle = Mathf.LerpAngle(transform.eulerAngles.z, maxAngle, Time.deltaTime * speedRotation);
        else
            angle = Mathf.LerpAngle(transform.eulerAngles.z, -maxAngle, Time.deltaTime * speedRotation);

        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
