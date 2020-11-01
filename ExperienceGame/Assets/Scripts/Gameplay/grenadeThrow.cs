using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrow : MonoBehaviour{

    public float throwForce = 12f;
    public GameObject grenadePrefab;

    void Start(){
        
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.G)){
         ThrowGrenade();   
        }
    }

    void ThrowGrenade(){
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        //GameObject grenadeObject = ObjectPoolingManager.Instance.GetGrenades ();
        
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

}
