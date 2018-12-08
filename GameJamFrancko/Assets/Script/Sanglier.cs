using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanglier : MonoBehaviour {

    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected Vector2 _direction;
    protected Animator _anim;
    protected BoxCollider2D _collider;
	void Start () {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._sanglierDeath);
    }
	

	void Update () {
        Move();
        if (_hp <= 0)
            Death();

	}

    protected void Move()
    {
        transform.Translate(_direction*_speed*Time.deltaTime,Space.World);
        transform.Rotate(Vector3.forward,Vector2.SignedAngle(transform.TransformDirection(Vector2.down),_direction));
    }

    protected void Death()
    {
        _collider.enabled = false;
        _speed = 0;
        if (_anim != null)
        {
            _anim.SetTrigger("deathTrigger");
            TutoControler._myTuto.ValidState(2);

            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("flaquesang"))
            {

                Instantiate(Resources.Load("PickableBody"), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }else Destroy(gameObject);
        GameControler._gc._sangliers.Remove(this.gameObject);
    }

    public void DealDammage(int amount)
    {
        _hp -= amount;
    }

    

}
