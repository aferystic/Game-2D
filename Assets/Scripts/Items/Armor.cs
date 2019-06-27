using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType {Helmet,Shoulders,Chest,Legs, Belt,Gloves,Boots,Ring, Necklace };
[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 1)]
public class Armor : Equipment
{
    [SerializeField]
    private ArmorType armorType;
    public ArmorType myArmorType
    {
        get => armorType;
        set { armorType = value; }
    }
}
