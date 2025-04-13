using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plants : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] bool isReplace = false;
    [SerializeField] bool isAttacking = false;

    [SerializeField] int id;
    [SerializeField] float price;
    [SerializeField] float health;
    [SerializeField] float rate = 2f;
    [SerializeField] float detectDistance = 9f;
    [SerializeField] float damage = 10f;
    //[SerializeField] float timeDoubleClick = 0.25f;

    [Header("Objects")]
    [SerializeField] Vector3 posConstruction;
    [SerializeField] GameObject canvasPlants;
    [SerializeField] Slider sliderHealthBar;

    //private float lastClickTime = 0f;
    private float healthDefault = 0f;

    private Vector3 _defaultPos;
    private Vector3 _posMouse;
    private RaycastHit2D _raycastHit2D;

    private GameObject _bullet;
    private GameObject _canvasPlants;
    private GameObject _construction;

    private Zombies _enemies;
    private PlantsZombiesScene _plantsZombiesScene;
    private void Awake()
    {
        _canvasPlants = Instantiate(canvasPlants);
        _canvasPlants.transform.SetParent(this.transform);
        _canvasPlants.transform.position = new Vector3(this.transform.position.x, 0.25f, 0);
    }

    private void Start()
    {
        _plantsZombiesScene = GameObject.FindGameObjectWithTag(TagName.TAG_GAME_CONTROLLER).GetComponent<PlantsZombiesScene>();

        isReplace = false;

        healthDefault = health;

        foreach (Transform child in _canvasPlants.transform)
            sliderHealthBar = child.GetComponent<Slider>();

        sliderHealthBar.maxValue = healthDefault;
        sliderHealthBar.value = healthDefault;
    }

    private void Update()
    {
        if (!isReplace && Input.GetKeyDown(KeyCode.Mouse1))
        {
            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
            _plantsZombiesScene.Gold += price;
            return;
        }

        if (this.health <= 0)
        {
            SetDefaultStats();
            SetDefaultConstruction();

            SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
        }
        else
            this.SetSliderHealthBar();

        this.DetectTarget();
    }

    private void FixedUpdate()
    {
        if (!isReplace)
        {
            this.gameObject.transform.position = this.PosMouse();
            return;
        }
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
        {
            //if (Time.time - lastClickTime < timeDoubleClick)
            //{
            //    SetDefaultStats();
            //    SetDefaultConstruction();

            //    SpawnPlantsZombies.Instance.EnqueueObj(this.gameObject);
            //    _plantsZombiesScene.Gold += Mathf.Round(Price * 0.75f);
            //}

            //lastClickTime = Time.time;

            return;
        }
        else
            posConstruction = _defaultPos;
    }

    private void OnMouseUp()
    {
        if (isReplace == true)
            return;

        if (_construction != null && !_construction.GetComponent<ConstructionPoints>().IsFull)
        {
            AudioManager.Instance.PlaceSound();

            this.gameObject.transform.position = new Vector3(
                this._construction.transform.position.x,
                this._construction.transform.position.y + this.GetComponent<SpriteRenderer>().size.x / 2);

            _construction.GetComponent<SpriteRenderer>().color = Color.clear;
            _construction.GetComponent<ConstructionPoints>().IsFull = true;

            isReplace = true;
            _plantsZombiesScene.IsPickup = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(detectDistance, 0));
    }

    public float Health
    {
        get => this.health;
        set => this.health = value;
    }

    public int ID
    {
        get => this.id;
        set => this.id = value;
    }

    public float Price
    {
        get => this.price;
        set => this.price = value;
    }

    public bool IsReplace
    {
        get => this.isReplace;
        set => this.isReplace = value;
    }

    public float Damage
    {
        get => this.damage;
        set => this.damage = value;
    }

    public void SetDefaultStats()
    {
        isReplace = false;
        health = healthDefault;

        sliderHealthBar.value = health;

        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetDefaultConstruction()
    {
        if (_construction == null)
            return;

        _construction.GetComponent<SpriteRenderer>().color = Color.white;
        _construction.GetComponent<ConstructionPoints>().IsFull = false;
    }

    private void DetectTarget()
    {
        _raycastHit2D = Physics2D.Raycast(this.transform.position, Vector2.right, detectDistance, LayerMask.GetMask(TagName.TAG_ENEMIES));

        if (_raycastHit2D.collider != null && !isAttacking)
        {
            _enemies = _raycastHit2D.collider.gameObject.GetComponent<Zombies>();

            if (isReplace && !isAttacking)
            {
                StartCoroutine(nameof(CoroutineStartAttack), _enemies);
                isAttacking = true;
            }
        }

        if (_raycastHit2D.collider == null && isAttacking)
        {
            StopCoroutine(nameof(CoroutineStartAttack));
            isAttacking = false;
        }
    }

    private void SetSliderHealthBar()
    {
        sliderHealthBar.value = this.health;

        if (health < healthDefault / 4)
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.red;
        else if (health < healthDefault / 2)
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.yellow;
        else
            sliderHealthBar.gameObject.GetComponent<Slider>().fillRect.GetComponent<Image>().color = Color.green;
    }

    private IEnumerator CoroutineStartAttack(Zombies zombies)
    {
        while (zombies.Health > 0)
        {
            _bullet = SpawnPlantsZombies.Instance.DequeueObj(TagName.TAG_BULLETS);
            _bullet.GetComponent<BulletPlants>().Plant = this;
            _bullet.transform.position = this.transform.position;
            _bullet.SetActive(true);

            AudioManager.Instance.FireBulletSound();

            yield return new WaitForSeconds(rate);
        }

        isAttacking = false;
    }

    private Vector3 PosMouse()
    {
        _posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _posMouse.z = 0;
        return _posMouse;
    }
}
