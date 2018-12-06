using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanglierAttaque : Sanglier {

    [SerializeField]
    private float _cooldown;


    private Transform _currentLapin;
    private float _timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_timer > 0) _timer -= Time.deltaTime;
        Search();
        Move();
        Debug.Log(_currentLapin);
	}

    private void Search()
    {
        if(_currentLapin != null)_direction = (_currentLapin.position - transform.position).normalized;
        else _direction = Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && _timer <=0 ) _currentLapin = collision.transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && _timer<=0)
        {
            _timer = _cooldown;
            _currentLapin = null;
        }
    }
}
