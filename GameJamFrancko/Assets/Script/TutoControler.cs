using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoControler : MonoBehaviour {

    public static TutoControler _myTuto;

    public int _tutoState { get; private set; }

    [SerializeField]
    private Text text;

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
            switch (state)
            {
                case 2:
                    text.text = "Éliminez-le";
                    break;
                case 3:
                    text.text = "La catapulte est vide ! Vous devez recharger !";
                    break;
                case 4:
                    text.text = "Servez vous des cadavres comme munition";
                    break;
                case 5:
                    text.text = "Cet ennemi est plus résistant ";
                    break;
                case 6:
                    text.text = "Les sangliers chargent !";
                    break;
            }
        }
    }

}
