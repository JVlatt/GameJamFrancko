using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulte : MonoBehaviour {

    private GameObject _ammoObject;
    public bool _isLoaded { get; private set;}
    public GameObject _target;

    [SerializeField]
    private float _speed;


    public void Reload(GameObject ammo)
    {
        if(_isLoaded == false)
        { 
            ammo.transform.parent = null;
            ammo.transform.position = transform.position;
            _isLoaded = true;
            _ammoObject = ammo;
        }
    }

    public void Shoot()
    {
        _ammoObject.GetComponent<Projectile>().FlyTo(_target.transform.position);
        _ammoObject = null;
        _isLoaded = false;
    }

    private void Update()
    {
        MoveTarget();
    }

    private void MoveTarget()
    {
        Vector3 _move = new Vector3(Input.GetAxis("RStickX"), Input.GetAxis("RStickY"));
        if (_move.magnitude >= 0.2f)
        {
            _target.transform.Translate(_move * _speed * Time.deltaTime, Space.World);
        }
    }
}
