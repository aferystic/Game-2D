using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStackableLoot : Loot
{

    [SerializeField]
    string[] lootNames;
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
        int length = lootNames.Length;
        if (length > 0)
        {
            int rand = Random.Range(0, length);
            QuestStackableItem armor = (QuestStackableItem)Instantiate(Resources.Load(lootNames[rand]));

            if (backpack.AddItem(armor))
            {
                this.gameObject.SetActive(false);
            }
        }

        base.AddItem();
    }
}
