using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]

public class WeaponData : ScriptableObject
{
    [Header("Name")]
    public new string name;

    [Header("Damage")]
    public float damage;
    public float maxDistance;

    [Header("RPM")]
    public float fireRate;
}
