using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{

    // modify the text objects via the Update...() functions below
    private TMP_Text textAmmo, textHealth;

    // modify via SetHUDPosition
    private RectTransform textAmmoRectTransform, textHealthRectTransform;

    public HUD()
    {
        // text mesh pro objects
        textAmmo   = GameObject.Find("text_ammo").GetComponentInChildren<TMP_Text>();
        textHealth = GameObject.Find("text_health").GetComponentInChildren<TMP_Text>();

        // the rect transform of the text mesh pro objects - used for positioning
        textAmmoRectTransform   = textAmmo.GetComponent<RectTransform>();
        textHealthRectTransform = textHealth.GetComponent<RectTransform>();
    }

    public void UpdateAmmo(int ammo)
    {
        textAmmo.text = $"AMMO   {ammo}";
    }

    public void UpdateHealth(int health)
    {
        textHealth.text = $"HEALTH {health}%";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // update the position of the HUD elements
    // should be run on setup, or when changing screen resolution
    // pass in the screen width, and screen height
    public void SetHUDPosition(int newWidth, int newHeight)
    {
        // all HUD elements will have the same X position
        float newTextX = -(newWidth / 2) + textAmmo.preferredWidth;

        // calculate the new Y position for each element
        float newAmmoTextY   = (newHeight / 2) - textAmmo.preferredHeight * 2;
        float newHealthTextY = newAmmoTextY - textHealth.preferredHeight;

        // set the new position of the elements
        textAmmoRectTransform.anchoredPosition   = new Vector3(newTextX, newAmmoTextY,   0);
        textHealthRectTransform.anchoredPosition = new Vector3(newTextX, newHealthTextY, 0);
    }
}
