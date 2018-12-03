using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider;

    private int _life;
    [SerializeField]
    private float _speed;

	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
	}
	
	
	void Update () {

        Move();

	}

    private void Move()
    {
        float _moveX = Input.GetAxis("Horizontal");
        float _moveY = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(_moveX * _speed, _moveY * _speed));
    }
}
