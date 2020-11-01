using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    public Character character;

    [Header("UI")]
    public Slider hpBar1;
    public Slider hpBar2;
    public TextMeshProUGUI hpText;

    void Update()
    {
        transform.LookAt(Camera.main.transform);

        if (hpBar1 != null && hpBar2 != null && hpText != null)
        {
            hpBar1.value = Mathf.Lerp(hpBar1.value, character.Health / character.MaxHealth, Time.deltaTime * 5f);
            hpBar2.value = Mathf.Lerp(hpBar2.value, character.Health / character.MaxHealth, Time.deltaTime * 5f);
            hpText.text = ((int)character.Health).ToString();
        }
    }
}
