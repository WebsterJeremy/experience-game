using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region AccessVariables


    #endregion
    #region PrivateVariables


    protected Area area;


    #endregion
    #region Initlization


    protected override void Start()
    {
        base.Start();
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
