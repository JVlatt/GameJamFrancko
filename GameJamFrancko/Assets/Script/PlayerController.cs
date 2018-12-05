using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;

    private bool _isHolding = false;
    private bool _canReload = false;

    private GameObject _pickedObject;
    private GameObject _currentCatapulte;

    private float m_lastPressed;

    private int _life;
    [SerializeField]
    private KeyCode _shoot;
    [SerializeField]
    private KeyCode _pickup;
    [SerializeField]
    private string[] _playerController = new string[2];
    [SerializeField]
    private float _speed;

    

	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        m_lastPressed = Time.time;
	}
	
	
	void Update () {

        Move();
        if(_isHolding)
        {          
            if(Input.GetKeyDown(_pickup))
            {
                if(_canReload)
                {
                    _currentCatapulte.GetComponent<Catapulte>().Reload(_pickedObject);
                    _pickedObject = null;
                    _isHolding = false;
                }
                else
                {
                    PutDown();
                }
            }
        }
        else if(_canReload && _currentCatapulte.GetComponent<Catapulte>()._isLoaded && Input.GetKeyDown(_shoot))
        {
            _currentCatapulte.GetComponent<Catapulte>().Shoot();
        }

	}

    private void Move()
    {
        float _moveX = Input.GetAxis(_playerController[0]);
        float _moveY = Input.GetAxis(_playerController[1]);
        transform.Translate(new Vector2(_moveX * _speed, _moveY * _speed));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Pickable" && Input.GetKeyUp(_pickup) && !_isHolding)
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            Debug.Log("Trigger");
            Pickup(collision.gameObject);
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Catapulte")
        {
            _currentCatapulte = collision.gameObject;
            _canReload = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Catapulte")
        {
            _currentCatapulte = null;
            _canReload = false;
        }
    }

    private void Pickup(GameObject _pickable)
    {
        _isHolding = true;
        _pickable.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        _pickable.gameObject.transform.parent = transform;
        _pickable.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,0,1));
        _pickedObject = _pickable;
    }
    private void PutDown()
    {
        _pickedObject.transform.position = transform.position;
        _pickedObject.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        _isHolding = false;
        _pickedObject.transform.parent = null;
    }
}
