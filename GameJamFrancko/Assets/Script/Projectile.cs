using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 _origin;
    private Vector3 _target;
    private float _speed = 4;
    private bool _canMove = false;
    private bool _canDammage = false;

    [SerializeField]
    private int _damage;


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
        _collider.enabled = false;
    }

    private void Update()
    {

        if (_canMove)
        { 
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }
        if (transform.position.x >= _target.x -1.0f)
        {
            _canDammage = true;
            _collider.enabled = true;
        }
        if(transform.position == _target)
        {
            _canMove = false;
            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        _canDammage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ennemi" && _canDammage)
        {
            collision.GetComponent<Sanglier>().DealDammage(1);
            Destroy(gameObject);
        }
    }
}
