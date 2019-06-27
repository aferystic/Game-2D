using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum WeaponType {Sword,Mace,Hammer,Dagger,Axe,Spear,Staff, Shield };
[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Equipment
{
    [SerializeField]
    private WeaponType weaponType;
    public WeaponType myWeaponType
    {
        get => weaponType;
        set { weaponType = value; }
    }
    [SerializeField]
    private float minDamage;
    public float myMinDamage
    {
        get => minDamage;
        set { minDamage = value; }
    }
    [SerializeField]
    private float maxDamage;
    public float myMaxDamage
    {
        get => maxDamage;
        set { maxDamage = value; }
    }

    public override string GetDescription()
    {
        if (minDamage != 0 && maxDamage != 0)
            return base.GetDescription() + string.Format("\n{0}-{1} Damage", minDamage, maxDamage);
        else return base.GetDescription();
    }
}
