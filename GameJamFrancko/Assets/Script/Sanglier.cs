using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanglier : MonoBehaviour {

    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _hp;
    protected Vector2 _direction;
	void Start () {
        _direction = Vector2.left;
	}
	

	void Update () {
        Move();
	}

    protected void Move()
    {
        transform.Translate(_direction*_speed,Space.World);
        if(Math.Abs( Vector2.Angle(Vector2.right, _direction))>1) transform.Rotate(Vector3.forward,Vector2.SignedAngle(transform.TransformDirection(Vector2.right),_direction));
    }

}
