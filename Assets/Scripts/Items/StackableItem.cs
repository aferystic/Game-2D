using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : Item
{
    protected int StackQuantity, MaxStackQuantity;
    public StackableItem()
    {
        StackQuantity = 0;
        MaxStackQuantity = 10;
    }
    public void ReduceQuantity()
    {
        StackQuantity--;
        if (StackQuantity <= 0)
        {
            Remove();
        }
    }
    public bool IncreaseQuantity()
    {
        StackQuantity++;
        if (StackQuantity > MaxStackQuantity)
        {
            StackQuantity = MaxStackQuantity;
            return false;
        }
        return true;
    }
    public int GetStackQuantity()
    {
        return StackQuantity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override string GetDescription()
    {
        return base.GetDescription();
    }
}
