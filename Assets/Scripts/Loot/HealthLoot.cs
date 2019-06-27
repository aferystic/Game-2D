using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : Loot
{
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
        HealthPotion healthPotion = (HealthPotion)Instantiate(Resources.Load("HealthPotion"));

        if (backpack.AddItem(healthPotion))
        {
            this.gameObject.SetActive(false);
        }
        base.AddItem();
    }
    
}
