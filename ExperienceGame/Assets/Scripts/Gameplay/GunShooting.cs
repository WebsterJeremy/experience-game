using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour{
    public GunFireAudio gunFireAudioPublic;
    private AudioSource source;
    Animator m_animator;
    public AudioClip m_fireSound;
    public AudioClip m_reloadSound;

    // Start is called before the first frame update
    void Start(){
        m_animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0) && GameController.IsPlaying()){
            m_animator.SetTrigger("Shoot");
            //source.Play();
            PlayFireSound();
        }
        
        if (Input.GetKey("r")){
            PlayReloadSound();
        }
    }

    private void PlayFireSound(){
        source.clip = m_fireSound;
        source.Play();
    }

    private void PlayReloadSound(){
        source.clip = m_reloadSound;
        source.Play();
    }
}
