using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    #region AccessVariables


    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textAmmo;
    [SerializeField] private TextMeshProUGUI textWeaponName;

    [SerializeField] private Image imgWeaponIcon;


    #endregion
    #region PrivateVariables


    private RectTransform rectHealth;
    private RectTransform rectAmmo;
    private RectTransform rectWeaponName;

    private int displayedHealth = 0;
    private int displayedAmmo = 0;


    #endregion
    #region Initlization


    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        DisplayHealth((int) Player.Instance.Health);
        DisplayAmmo((int) Player.Instance.GetWeapon().GetAmmoCount());
    }


    #endregion
    #region Getters & Setters

    public void ChangeAmmo(int ammo) {
        StartCoroutine(_SetAmmo(displayedAmmo, ammo, 0.2f));
    }

    public void ChangeHealth(int health) {
        StartCoroutine(_SetHealth(displayedHealth, health, 0.2f));
    }


    #endregion
    #region Core


    private void DisplayAmmo(int ammo)
    {
        textAmmo.text = "AMMO: "+ ammo.ToString() +" / "+ Player.Instance.GetWeapon().GetWeaponData().GetAmmoClip().ToString() +" ("+ Player.Instance.GetAmmo(Player.Instance.GetWeapon().GetWeaponData().GetAmmoType()).ToString() +")";
//        rectAmmo.sizeDelta = new Vector2(100 + (textAmmo.text.Length * 40), rectAmmo.sizeDelta.y);
        displayedAmmo = ammo;
    }

    private void DisplayHealth(int health)
    {
        textHealth.text = "HEALTH: "+ health.ToString();
//        rectHealth.sizeDelta = new Vector2(100 + (textHealth.text.Length * 40), rectHealth.sizeDelta.y);
        displayedHealth = health;
    }


    #endregion
    #region Animation


    
    IEnumerator _SetAmmo(int startValue, int endValue, float duration)
    {
        float animTime = 0f;
        float ammo = startValue;

        while (animTime < duration)
        {
            ammo = Mathf.Lerp(ammo, endValue, animTime / duration);
            DisplayAmmo((int)ammo);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
    }

    IEnumerator _SetHealth(int startValue, int endValue, float duration)
    {
        float animTime = 0f;
        float health = startValue;

        while (animTime < duration)
        {
            health = Mathf.Lerp(health, endValue, animTime / duration);
            DisplayHealth((int)health);

            yield return new WaitForEndOfFrame();
            animTime += Time.deltaTime;
        }
    }


    #endregion
}
