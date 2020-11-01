using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region AccessVariables


    public Camera playerCamera;


    #endregion
    #region PrivateVariables


    protected Area area;


    #endregion
    #region Initlization


    private static Player instance;
    public static Player Instance // Assign Singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("Player");
                    instance = instanceContainer.AddComponent<Player>();
                }
            }
            return instance;
        }
    }

    protected override void Start()
    {
        base.Start();

        if (GetWeapons().Count > 0)
        {
            SetWeapon(GetWeapons()[0]);
        }
    }


    #endregion
    #region Getters & Setters

    public Area Area { get { return area; } }

    #endregion
    #region Input


    private void Update()
    {
        if (GameController.IsPlaying())
        {
            if (Input.GetMouseButtonDown(0)) // Check if Weapon is Automatic, if not don't allow holding
            {
                GetWeapon().Fire();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetWeapon().AimDownSights();
            }
            else if (Input.GetKey("r"))
            {
                GetWeapon().Reload();
            }
        }
    }


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

        if (other.CompareTag("TriggerDeath"))
        {
            Death();
        }

        if (other.CompareTag("AmmoCrate"))
        {
            AmmoCrate ammoCrate = other.GetComponent<AmmoCrate>();

            if (ammoCrate != null)
            {
                int leftOverAmmo = GiveAmmo(ammoCrate.ammoType, ammoCrate.ammo);

                if (leftOverAmmo <= 0)
                {
                    Destroy(ammoCrate.gameObject);
                }
                else
                {
                    ammoCrate.ammo = leftOverAmmo;
                }
            }
        }
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

    protected override void UpdateHealthbar()
    {

    }

    public override int GiveAmmo(AmmoType ammoType, int amount)
    {
        int leftOverAmmo = base.GiveAmmo(ammoType, amount);

        UIController.Instance.GetHUD().ChangeAmmo((int)Player.Instance.GetWeapon().GetAmmoCount());

        return leftOverAmmo;
    }

    #endregion
}
