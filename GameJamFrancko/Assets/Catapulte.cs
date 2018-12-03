using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulte : MonoBehaviour {

    private GameObject _ammoObject;
    public bool _isLoaded { get; private set;}

    public void Reload(GameObject ammo)
    {
        ammo.transform.parent = null;
        ammo.transform.position = transform.position;
        _isLoaded = true;
        _ammoObject = ammo;
    }

    public void Shoot()
    {

        _ammoObject.GetComponent<Projectile>().FlyTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _ammoObject = null;
        _isLoaded = false;
    }
}
