using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireAudio : MonoBehaviour{
private AudioSource source;
public AudioSource foo { get {return source;}}

    // Start is called before the first frame update
    void Start(){
        source = GetComponent<AudioSource>();
        //    public int Ammo { get {return ammo;}}
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void playSound(){
        source.Play();
    }
}
