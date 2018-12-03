using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 _origin;
    private Vector3 _target;
    private float _speed = 4;
    private bool _canMove = false;

    public void FlyTo(Vector3 target)
    {
        _target = target;
        _target.z = 0;
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }
}
