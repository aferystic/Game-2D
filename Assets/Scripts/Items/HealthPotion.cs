using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : StackableItem, IUseable
{
    [SerializeField]
    private int health = 10;

    public HealthPotion()
    {
        StackQuantity = 1;
    }
    public void Use()
    {
        if (Player.myInstance.myHealth.myCurrentValue != Player.myInstance.myHealth.myMaxValue)
        {
            Player.myInstance.ChangeHealth(health);
            ReduceQuantity();
        }
    }
    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n+{0} Health on use", health);
    }
}
