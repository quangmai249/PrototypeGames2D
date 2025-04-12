using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimations : MonoBehaviour
{
    [SerializeField] float timeScale = 0.5f;

    private Vector3 _localScaleDefault;

    void Start()
    {
        _localScaleDefault = this.transform.localScale;
    }

    private void OnMouseEnter()
    {
        this.transform.DOScale(_localScaleDefault * 1.15f, this.timeScale);
    }

    private void OnMouseExit()
    {
        this.transform.DOScale(_localScaleDefault, this.timeScale);
    }
}
