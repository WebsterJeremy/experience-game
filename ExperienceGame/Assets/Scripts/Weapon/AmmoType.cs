using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Gameplay/Ammo")]
public class AmmoType : ScriptableObject
{
    [Header("Data")]
    [SerializeField] private string title = "Pistol";
    [SerializeField] private int maxCapacity = 54;

    public int GetMaxCapacity() { return maxCapacity; }

    void Awake()
    {

    }
}

