using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected Quality quality;
    public string GetItemName()
    {
        return itemName;
    }
    public void SetItemName(string name)
    {
        itemName = name;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    //Handler to the slot an item is currently on.
    private Slot slot;

    public Slot Slot { get => slot; set => slot = value; }

    public void Remove()
    {
        if (slot != null)
        {
            slot.RemoveItem();
            Backpack.MyInstance.onItemCountChanged(this); //update item
        }
    }
    public virtual string GetDescription()
    {
        
        return string.Format("<b><size=10><color={0}>{1}</color></size></b>", QualityColor.GetColor(quality),itemName);
    }
}
