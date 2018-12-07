using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour {

    public AudioClip _catapulte;
    public AudioClip _sanglierDeath;
    public AudioClip _rire;
    public AudioClip _music;
    public AudioClip _pickup;
    public AudioClip _drop;
    
    public static SoundControler _soundControler;

    private AudioSource _source;

    private void Awake()
    {
        if (_soundControler == null)
            _soundControler = this;
        else
            Destroy(this);

        _source = GetComponent<AudioSource>();
        
        _source.clip = _music;
        _source.loop = true;
        _source.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        _source.PlayOneShot(sound);
    }

}
