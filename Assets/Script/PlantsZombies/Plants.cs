using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plants : MonoBehaviour
{
    [SerializeField] bool isReplace = false;
    [SerializeField] int id;
    [SerializeField] Vector3 posConstruction;

    private Vector3 _defaultPos;
    private GameObject _construction;
    private PosShoppingDefault _posShoppingDefault;

    private void Start()
    {
        _posShoppingDefault = GameObject.FindGameObjectWithTag(TagName.TAG_POS_SHOPPING_DEFAULT).GetComponent<PosShoppingDefault>();
        _defaultPos = _posShoppingDefault.transform.position;

        isReplace = false;
    }

    public int ID
    {
        get => this.id;
        set => this.id = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_CONSTRUCTION))
        {
            _construction = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_CONSTRUCTION))
        {
            _construction = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.TAG_CONSTRUCTION))
        {
            posConstruction = _defaultPos;

            if (_construction != null && _construction.GetComponent<ConstructionPoints>().IsFull == false)
            {
                _construction.GetComponent<SpriteRenderer>().color = Color.white;
                _construction = null;
            }
        }
    }

    private void OnMouseDown()
    {
        if (isReplace == true)
            return;

        this.posConstruction = _defaultPos;
    }

    private void OnMouseUp()
    {
        if (isReplace == true)
            return;

        if (_construction != null && !_construction.GetComponent<ConstructionPoints>().IsFull)
        {
            this.gameObject.transform.position = new Vector3(
                this._construction.transform.position.x,
                this._construction.transform.position.y + this.GetComponent<SpriteRenderer>().size.x / 2);

            Debug.Log(this.GetComponent<SpriteRenderer>().size.x);

            _construction.GetComponent<SpriteRenderer>().color = Color.clear;
            _construction.GetComponent<ConstructionPoints>().IsFull = true;

            isReplace = true;
        }
        else
            this.gameObject.transform.position = _defaultPos;
    }

    private void OnMouseDrag()
    {
        if (isReplace == true)
            return;

        this.gameObject.transform.position
            = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }
}
