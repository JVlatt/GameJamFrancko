﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;

    private bool _isHolding = false;
    private bool _canReload = false;

    private GameObject _pickedObject;
    private GameObject _currentCatapulte;

    private int _life;
    [SerializeField]
    private float _speed;

    

	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
	}
	
	
	void Update () {

        Move();
        if(_isHolding)
        {          
            if(Input.GetKeyDown(KeyCode.E))
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
        else if(_canReload && _currentCatapulte.GetComponent<Catapulte>()._isLoaded && Input.GetKeyDown(KeyCode.X))
        {
            _currentCatapulte.GetComponent<Catapulte>().Shoot();
        }

	}

    private void Move()
    {
        float _moveX = Input.GetAxis("Horizontal");
        float _moveY = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(_moveX * _speed, _moveY * _speed));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Pickable" && Input.GetKeyDown(KeyCode.E) && !_isHolding)
        {
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
        _pickable.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        _pickable.gameObject.transform.parent = transform;
        _pickable.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0,0,1));
        _pickedObject = _pickable;
        StartCoroutine("Wait");
    }
    private void PutDown()
    {
        _pickedObject.transform.position = transform.position;
        _pickedObject.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
        _isHolding = false;
        _pickedObject.transform.parent = null;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        _isHolding = true;
    }
}