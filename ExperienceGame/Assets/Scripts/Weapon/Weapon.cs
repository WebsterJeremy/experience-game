using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Character character;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform shootPos;

    private Animator animator;
    private AudioSource audioSource;
    private ParticleSystem muzzelFlash;

    private int ammoCount;
    private float firedLast;


    public IEnumerator Start()
    {
        animator = character.GetComponent<Animator>();
        audioSource = character.GetComponent<AudioSource>();

        ammoCount = Mathf.Clamp(weaponData.GetInitalAmmoCount(), 0, weaponData.GetAmmoClip());
        firedLast = 0;

        yield return new WaitForSeconds(0.1f);
        if (weaponData.GetInitalAmmoCount() > weaponData.GetAmmoClip()) Player.Instance.GiveAmmo(weaponData.GetAmmoType(), weaponData.GetInitalAmmoCount() - ammoCount);

    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }

    public WeaponData GetWeaponData()
    {
        return weaponData;
    }

    public void Fire()
    {
        if (Time.time < firedLast + weaponData.GetFirerate()) return;
        firedLast = Time.time;

        if (ammoCount <= 0)
        {
            Reload();
            // Play out of ammo in clip sound effect?
            return;
        }

 //       animator.SetTrigger("Shoot");

        audioSource.clip = weaponData.GetFireSound();
        audioSource.Play();

        ammoCount--;

        UIController.Instance.GetHUD().ChangeAmmo(ammoCount);

        GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
        bulletObject.transform.position = shootPos.transform.position + shootPos.transform.forward;
        bulletObject.transform.forward = shootPos.transform.forward;
    }

    public void AimDownSights()
    {

    }

    public void Reload()
    {
        if (character.GetAmmo(weaponData.GetAmmoType()) > 0 && ammoCount != weaponData.GetAmmoClip())
        {
            int addedAmmo = Mathf.Clamp(weaponData.GetAmmoClip() - ammoCount, 0, Player.Instance.GetAmmo(weaponData.GetAmmoType()));

            ammoCount = ammoCount + addedAmmo;
            Player.Instance.SetAmmo(weaponData.GetAmmoType(), Player.Instance.GetAmmo(weaponData.GetAmmoType()) - addedAmmo);

            audioSource.clip = weaponData.GetReloadSound();
            audioSource.Play();

            UIController.Instance.GetHUD().ChangeAmmo(ammoCount);
        }
    }

    public void Pickup()
    {

    }

    public void Drop()
    {

    }
}
