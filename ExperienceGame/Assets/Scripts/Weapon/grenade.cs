using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour{

    public GameObject explosionEffect;

    public float blastRadius = 50f;
    public float Eforce = 700f;
    public float delay = 2f;
    float countDown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start(){
        countDown = delay;
    }

    // Update is called once per frame
    void Update(){
        countDown -= Time.deltaTime;
        if (countDown <= 0f && !hasExploded){
            Explode();
            hasExploded = true;
        }
    }

    void Explode(){
        // show some effect
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        
        // add force to blast objects in colliders array

        foreach (Collider hit in colliders){
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            Debug.Log(rb);
            if (rb != null){
                rb.AddExplosionForce(Eforce, transform.position, blastRadius);
            }
        }


        // foreach (Collider nearbyObject in colliders){
        //     Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
        //     if (rb != null){
        //         rb.AddExplosionForce(force, transform.position, blastRadius);
        //     }
        // }

        // get nearby objects
            // add force
            // Damage
        // Remove Grenade
       //Destroy(gameObject);
       //Destroy(explosionEffect);
    }
}
