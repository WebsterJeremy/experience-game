using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Gameplay/Weapon")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType { Primary, Secondary, Grenade }

    [Header("Data")]
    [SerializeField] private string title = "Unknown";
    [SerializeField] private float damage = 10f;
    [SerializeField] private float firerate = 1f;
    [SerializeField] private float recoil = 0f;
    [SerializeField] private float spread = 0.5f;

    [SerializeField] private int initalAmmoCount = 30;
    [SerializeField] private int ammoClip = 30;
    [SerializeField] private int maxAmmoCount = 60;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoUsage = 1;
    [SerializeField] private bool automatic = false;

    [SerializeField] private int bulletCount = 1;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float bulletPenetration = 1f;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private WeaponType weaponType = WeaponType.Primary;

    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip scopeSound;

    public string GetTitle() { return title; }
    public float GetDamage() { return damage; }
    public float GetFirerate() { return firerate; }
    public float GetRecoil() { return recoil; }
    public float GetSpread() { return spread; }

    public int GetInitalAmmoCount() { return initalAmmoCount; }
    public int GetAmmoClip() { return ammoClip; }
    public int GetMaxAmmoCount() { return maxAmmoCount; }
    public AmmoType GetAmmoType() { return ammoType; }
    public int GetAmmoUsage() { return ammoUsage; }
    public bool GetAutomatic() { return automatic; }

    public int GetBulletCount() { return bulletCount; }
    public float GetBulletSpeed() { return bulletSpeed; }
    public float GetBulletPenetration() { return bulletPenetration; }
    public GameObject GetBulletPrefab() { return bulletPrefab; }

    public WeaponType GetWeaponType() { return weaponType; }

    public AudioClip GetFireSound() { return fireSound; }
    public AudioClip GetReloadSound() { return reloadSound; }
    public AudioClip GetScopeSound() { return scopeSound; }

    void Awake()
    {

    }
}
