using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region AccessVariables
    public Camera playerCamera;
    public GameObject bulletPrefab;
    public int initialAmmo = 12;
    public int Ammo { get {return ammo;}}
    public ParticleSystem muzzelFlash;

    #endregion
    #region PrivateVariables
    private int ammo;

    protected Area area;

    #endregion
    #region Initlization

    protected override void Start()
    {
        base.Start();

        // player stats setup
        ammo  = initialAmmo;

        // HUD setup
        GameController.HUD.UpdateAmmo(ammo);
        GameController.HUD.UpdateHealth((int) this.health);
    }

    void Update(){
        // only fire if the game is playing and the player has ammo
        if (Input.GetMouseButtonDown(0) && GameController.IsPlaying() && ammo > 0)
        {
            Debug.Log("Firing!");

            muzzelFlash.Play();
            ammo--;

            // update the ammo count on the HUD
            GameController.HUD.UpdateAmmo(ammo);

            GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
            bulletObject.transform.position=playerCamera.transform.position + playerCamera.transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;
        }
    }

    // Check for collisions with AmmoCrate - Refill Ammo
    void OnControllerColliderHit (ControllerColliderHit hit){
        //Debug.Log(hit.gameObject.name);
        if(hit.collider.GetComponent<AmmoCrate> () != null){
            AmmoCrate ammoCrate = hit.gameObject.GetComponent<AmmoCrate> ();
            ammo += ammoCrate.ammo;

            // update the ammount count on the HUD
            GameController.HUD.UpdateAmmo(ammo);

            Destroy (ammoCrate.gameObject);
        }
    }

    #endregion
    #region Getters & Setters

    public Area Area { get { return area; } }

    #endregion
    #region Main

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Area"))
        {
            if (area != null && area.gameObject == other.gameObject) return;

            area = other.GetComponent<Area>();
            area.Enter();
        }
        else if (other.CompareTag("TriggerDeath"))
        {
            Death();
        }
    }

    protected override void UpdateHealthbar()
    {

    }

    protected override void Death()
    {
        if (dead) return;
        base.Death();

        Respawn();
    }

    public void Respawn()
    {
        if (area != null)
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = area.transform.position;
            transform.rotation = area.transform.rotation;
            GetComponent<CharacterController>().enabled = true;
        }

        dead = false;
    }

    #endregion
}
