using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Gun Info")]
    public new string name;
    [Header("Shot")]
    public float damage;
    public float maxDistance;
    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;
}
