using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    //[Header("Visuals")]
    public Camera playerCamera;
      
    //[Header"Gameplay"]
    public int initialAmmo = 12;
    private int ammo;
    public int Ammo { get { return ammo;}}  

    

    // Start is called before the first frame update
    void Start(){
        ammo = initialAmmo;
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(ammo > 0){
            ammo--;
            GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
            bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;
            }
                
        }

        if(Input.GetMouseButtonDown(1)){
            Debug.Log("Right Button Pushed");
        }
    }
    // Check for collisions
    void OnControllerColliderHit (ControllerColliderHit hit){
        Debug.Log(hit.gameObject.name);
        if(hit.collider.GetComponent<AmmoCrate> () != null){
            AmmoCrate ammoCrate = hit.gameObject.GetComponent<AmmoCrate> ();
            ammo += ammoCrate.ammo;
            Destroy (ammoCrate.gameObject);
        }
    }

}
