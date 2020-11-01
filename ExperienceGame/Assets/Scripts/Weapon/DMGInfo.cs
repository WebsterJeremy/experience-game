using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGInfo : MonoBehaviour
{
    public GameObject inflictor;
    public float damage;
    public float force;

    public DMGInfo(GameObject inflictor, float damage, float force)
    {
        this.inflictor = inflictor;
        this.damage = damage;
        this.force = force;
    }
}
