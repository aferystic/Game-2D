using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Equipment: Unstackableitem, IEquipable
{
    [SerializeField]
    private int strength;
    public int myStrength
    {
        get => strength;
        set { strength = value; }
    }
    //[SerializeField]
    private int intelligence;
    [SerializeField]
    private int agillity;
    public int myAgillity
    {
        get => agillity;
        set { agillity = value; }
    }
    [SerializeField]
    private int stamina;
    public int myStamina
    {
        get => stamina;
        set { stamina = value; }
    }

    public string Equip()
    {
        return CharacterPanel.MyInstance.equipItem(this);
    }

    public override string GetDescription()
    {
        string desc = string.Empty;
        if (strength > 0)
            desc += string.Format("\n+{0} Strength", strength);
        if (intelligence > 0)
            desc += string.Format("\n+{0} Intelligence", intelligence);
        if (agillity > 0)
            desc += string.Format("\n+{0} Agillity", agillity);
        if (stamina > 0)
            desc += string.Format("\n+{0} Stamina", stamina);
        return base.GetDescription() + desc;
    }
}
