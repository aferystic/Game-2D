using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorLoot :Loot
{

    [SerializeField]
    string[] armorNames;
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
        int length = armorNames.Length;
        if (length > 0)
        {
            int rand = Random.Range(0, length);
            Armor armor = (Armor)Instantiate(Resources.Load(armorNames[rand]));

            if (backpack.AddItem(armor))
            {
                this.gameObject.SetActive(false);
            }
        }
        base.AddItem();
    }
}
