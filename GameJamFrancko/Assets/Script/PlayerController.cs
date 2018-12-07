using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    private bool _isHolding = false;
    private bool _canReload = false;

    private GameObject _pickedObject;
    private GameObject _currentCatapulte;

    private float _timer;

    private int _life;

    [SerializeField]
    private KeyCode[] _playerControllerKeyCode = new KeyCode[2];
    [SerializeField]
    private string[] _playerControllerAxis = new string[2];
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _pickupCooldown;

    

	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
	}
	
	
	void Update () {

        if (_timer > 0) _timer -= Time.deltaTime;
        Move();
        if(_isHolding)
        {          
            if(Input.GetKeyDown(_playerControllerKeyCode[0]) && _timer<=0)
            {
                if(_canReload && !_currentCatapulte.GetComponent<Catapulte>()._isLoaded)
                {
                    _currentCatapulte.GetComponent<Catapulte>().Reload(_pickedObject);
                    _pickedObject = null;
                    _isHolding = false;
                }
                else
                {
                    _pickedObject.GetComponent<CircleCollider2D>().enabled = true;
                    PutDown();
                }
                _animator.SetBool("isHolding", _isHolding);
                _timer = _pickupCooldown;
            }
        }
        else if(_canReload && _currentCatapulte.GetComponent<Catapulte>()._isLoaded && Input.GetKeyDown(_playerControllerKeyCode[1]))
        {
            _currentCatapulte.GetComponent<Catapulte>().Shoot();
        }

	}

    private void Move()
    {
        Vector2 _move = new Vector2(Input.GetAxis(_playerControllerAxis[0]), Input.GetAxis(_playerControllerAxis[1]));

        if(_move.magnitude >= 0.2f)
        {
            _animator.SetBool("Move", true);
            transform.Translate(_move * _speed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward, Vector2.SignedAngle(transform.TransformDirection(Vector2.down), _move));
        }
            else _animator.SetBool("Move", false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Pickable" && Input.GetKeyDown(_playerControllerKeyCode[0]) && !_isHolding && _timer<=0)
        {
            collision.GetComponent<CircleCollider2D>().enabled = false;
            _timer = _pickupCooldown;
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
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._pickup);
        _isHolding = true;
        _animator.SetBool("isHolding", _isHolding);
        _pickable.gameObject.transform.parent = transform;
        _pickable.transform.localPosition = new Vector2(0,-0.25f);
        _pickable.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,0,1));
        _pickedObject = _pickable;
    }
    private void PutDown()
    {
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._drop);
        _pickedObject.transform.position = transform.position;
        _pickedObject.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        _isHolding = false;
        _pickedObject.transform.parent = null;
    }
}
