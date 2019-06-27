using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoot : Loot
{

    [SerializeField]
    string[] weaponNames;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (collision && Input.GetKeyDown(addItem))
        {
            AddItem();
        }
        base.Update();
    }

    protected override void AddItem()
    {
        int length = weaponNames.Length;
        if (length > 0)
        {
            int rand = Random.Range(0, length);
            Weapon weapon = (Weapon)Instantiate(Resources.Load(weaponNames[rand]));

            if (backpack.AddItem(weapon))
            {
                this.gameObject.SetActive(false);
            }
        }
        base.AddItem();
    }
}
