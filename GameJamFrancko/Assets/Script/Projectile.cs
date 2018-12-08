using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float _scaleAdd;

    private float _normalScale;
    private Vector3 _origin;
    private Vector3 _target;
    private float _speed = 4;
    private bool _canMove = false;
    private bool _canDammage = false;
    private Vector3 _middle;

    [SerializeField]
    private int _damage;


    private CircleCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _normalScale = transform.localScale.x;
    }

    public void FlyTo(Vector3 target)
    {
        _target = target;
        _target.z = 0;
        _canMove = true;
        _collider.enabled = false;
        _middle = (_target+transform.position)/2;
    }

    private void Update()
    {
        if (_canDammage == true) _canDammage = false;
        if (_canMove)
        { 
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            transform.Rotate(Vector3.forward, 30);
            if ((_middle - transform.position).x > 0)
            {
                transform.localScale += new Vector3(_scaleAdd, _scaleAdd);
            }else transform.localScale -= new Vector3(_scaleAdd, _scaleAdd);

            if (transform.position == _target)
            {
                _canMove = false;
                _canDammage = true;
                _collider.enabled = true;
                transform.localScale = new Vector3(_normalScale,_normalScale,_normalScale);
            }
        }
         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ennemi" && _canDammage && collision.isTrigger==false)
        {
            collision.GetComponent<Sanglier>().DealDammage(1);
            TutoControler._myTuto.ValidState(2);
            Destroy(gameObject);
        }
    }
}
