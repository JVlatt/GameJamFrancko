using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 _origin;
    private Vector3 _target;
    private float _speed = 4;
    private bool _canMove = false;


    private CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void FlyTo(Vector3 target)
    {
        _target = target;
        _target.z = 0;
        _canMove = true;
    }

    private void Update()
    {

        if (_canMove)
        { 
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }
        if (transform.position == _target)
        {
            _canMove = false;
            _collider.enabled = true;
        }
    }
}
