using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoControler : MonoBehaviour {

    public static TutoControler _myTuto;

    public int _tutoState { get; private set; }

    private Animator _myAnimator;


	// Use this for initialization
	void Awake () {
        _myTuto = this;
        _myAnimator = GetComponent<Animator>();
	}
	

    public void ValidState(int state)
    {
        if (_tutoState == state)
        {
            state++;
            _myAnimator.SetInteger("CurrentState", state);
        }
    }

}
