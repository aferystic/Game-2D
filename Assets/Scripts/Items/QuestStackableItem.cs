using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestStackableItem", menuName = "Items/QuestStackableItem", order = 1)]
class QuestStackableItem:StackableItem
{
    public QuestStackableItem()
    {
        StackQuantity = 1;
    }
}
