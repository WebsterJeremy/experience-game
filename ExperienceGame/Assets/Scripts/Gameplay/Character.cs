using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region AccessVariables

    [Header("Character")]
    [SerializeField] private string displayName = "Character";
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Weapon weapon;
    [SerializeField] public List<Weapon> weapons = new List<Weapon>();
    [SerializeField] public Dictionary<AmmoType, int> ammo = new Dictionary<AmmoType, int>();

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

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }

    public int GetAmmo(AmmoType ammoType)
    {
        if (ammo == null || !ammo.ContainsKey(ammoType)) return 0; 

        return ammo[ammoType];
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

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

    public virtual int GiveAmmo(AmmoType ammoType, int amount)
    {
        if (!ammo.ContainsKey(ammoType)) {
            ammo.Add(ammoType, amount);

            return 0;
        }
        else
        {
            if (ammo[ammoType] + amount > ammoType.GetMaxCapacity())
            {
                int leftOverAmmo = (ammo[ammoType] + amount) - ammoType.GetMaxCapacity();

                ammo[ammoType] = ammoType.GetMaxCapacity();

                return leftOverAmmo;
            }
            else
            {
                ammo[ammoType] = ammo[ammoType] + amount;

                return 0;
            }
        }
    }

    public virtual void SetAmmo(AmmoType ammoType, int amount)
    {
        if (!ammo.ContainsKey(ammoType))
        {
            ammo.Add(ammoType, amount);
        }
        else
        {
            if (amount > ammoType.GetMaxCapacity())
            {
                ammo[ammoType] = ammoType.GetMaxCapacity();

            }
            else
            {
                ammo[ammoType] = amount;
            }
        }
    }

    #endregion
}
