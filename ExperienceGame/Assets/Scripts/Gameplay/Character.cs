using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName = "Utility/Language")]
public class WeaponData : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private float damage;
}

public class Weapon : MonoBehaviour
{

    [Header("Weapon")]
    [SerializeField] private WeaponData weaponData;

    public void Fire()
    {

    }

    public void Reload()
    {

    }

    public void AimDownSights()
    {

    }

}

public class DMGInfo
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

public abstract class Character : MonoBehaviour
{
    #region AccessVariables

    [Header("Character")]
    [SerializeField] private string displayName = "Character";
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Weapon weapon;

    [Header("Sounds")]
    [SerializeField] private SoundController.Sound[] walkSounds;
    [SerializeField] private SoundController.Sound[] damagedSounds;
    [SerializeField] private SoundController.Sound[] deathSounds;

    #endregion
    #region PrivateVariables

    protected bool dead = false;
    protected float health;
    protected Animator animator;


    #endregion
    #region Initlization


    protected virtual void Start()
    {
        health = maxHealth;
        UpdateHealthbar();

        animator = GetComponent<Animator>();
    }


    #endregion
    #region Getters & Setters

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public Weapon GetWeapon() { return weapon; }

    #endregion
    #region Main

    public void TakeDamage(DMGInfo dmgInfo)
    {
        if (dead) return;

        health = Mathf.Clamp(this.health - Mathf.Abs(dmgInfo.damage), 0, maxHealth);

        if (animator != null) { animator.SetTrigger("DMG"); }
        SoundController.PlaySound(damagedSounds[Random.Range(0, damagedSounds.Length)]);

        UpdateHealthbar();

        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        if (dead) return;

        dead = true;
        health = 0;
        UpdateHealthbar();

        Debug.Log("You Died!");

        if (animator != null) { animator.SetTrigger("DIE"); }

        SoundController.PlaySound(deathSounds[Random.Range(0, deathSounds.Length)]);
//        Instantiate(GameplayController.Instance.enemyDeathEffect, transform.position, Quaternion.Euler(90, 0, 0));
    }

    protected abstract void UpdateHealthbar();

    #endregion
}
