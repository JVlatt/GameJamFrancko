using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 _origin;
    private Vector3 _target;
    private float _speed = 4;
    private bool _canMove = false;

    private float _scale;
    private float _growingScale;
    private float _topScale;

    private Vector3 _center;

    private bool _up = false;
    private bool _down = false;

    private CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _scale = transform.localScale.x;
        _growingScale = _scale;
        _topScale = _scale * 2;

    }

    public void FlyTo(Vector3 target)
    {
        _target = target;
        _target.z = 0;
        _center = Vector3.Lerp(transform.position,_target,0.5f);
        _canMove = true;
        _up = true;
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
