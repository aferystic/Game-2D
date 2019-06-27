using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    protected Item item;
    [SerializeField]
    private Image icon;
    private Text quantityText;


    public bool AddItem(Item item)
    {
        if (this.item == null)
        {
            this.item = item;
            icon.sprite = item.GetIcon();
            icon.color = Color.white;
            this.item.Slot = this;
            //item.Slot = this;

            //item.name = HealthPotion(Clone) -> change to HealthPotion below
            //item.SetItemName(item.name.Substring(0, item.name.Length - 7)); //cut '(Clone)' end of item name XD
            if (item is StackableItem)
                quantityText.text = (item as StackableItem).GetStackQuantity().ToString();
            return true;
        }
        else if (this.transform.parent.parent.gameObject.GetComponent<CharacterPanel>() != null)
        {
            CharacterPanel.MyInstance.subItemStats(this.item);
            Slot tempSlot = item.Slot;
            Item tempItem = this.item;
            this.item = item;
            this.icon.sprite = item.GetIcon();
            this.icon.color = Color.white;
            this.item.Slot = this;
            item = tempItem;
            tempSlot.icon.sprite = tempItem.GetIcon();
            tempSlot.icon.color = Color.white;
            tempSlot.item = tempItem;
            item.Slot = tempSlot;
            Debug.Log(this.item);
            Debug.Log(item);
            return false;
        }
        else return false;

    }

    public bool AddStackableItem()
    {
        if ((item as StackableItem).IncreaseQuantity())
        {
            quantityText.text = (item as StackableItem).GetStackQuantity().ToString();
            return true;
        }

        return false;
    }
    public bool IsEmpty()
    {
        if (item == null)
            return true;
        return false;
    }
    public Item GetItem()
    {
        return item;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
         if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item is IUseable)
            {
                (item as IUseable).Use();
                if (item is StackableItem && (item as StackableItem).GetStackQuantity() > 0)
                {
                    quantityText.text = (item as StackableItem).GetStackQuantity().ToString();
                    Backpack.MyInstance.onItemCountChanged(item); //update item
                } 
                else quantityText.text = "";
            }
            else if (item is IEquipable)
            {
                UIManager.MyInstance.HideTooltip();
                if (this.transform.parent.gameObject.GetComponent<Backpack>() == null)
                {
                    Item bufItem = item;
                    
                    Backpack.MyInstance.AddItem(item);

                    this.RemoveItem();
                    CharacterPanel.MyInstance.subItemStats(bufItem);

                }
                else
                {
                    CharacterPanel.MyInstance.addItemStats(item);
                    if ((item as IEquipable).Equip().Equals("equip"))
                        this.RemoveItem();
                    else UIManager.MyInstance.ShowTooltip(transform.position, item.GetDescription());
                }
                //Debug.Log("Equipable");


            }

        }
    }

    public bool RemoveItems(Item item)
    {
            if (item is StackableItem)
            {
                (item as StackableItem).ReduceQuantity();
                if ((item as StackableItem).GetStackQuantity() > 0)
                {
                    quantityText.text = (item as StackableItem).GetStackQuantity().ToString();
                    Backpack.MyInstance.onItemCountChanged(item); //update item
                    return true;
                }
                else
                {
                    quantityText.text = "";
                    Backpack.MyInstance.onItemCountChanged(item); //update item
                    return false;
                }
                    

            
            
            }
        return false;
        
    }

    public void RemoveItem()
    {
        this.item = null;
        icon.color = new Color(0, 0, 0, 0);
    }
    public void UseItem()
    {

    }
    void Start()
    {
        quantityText = GetComponentInChildren<Text>();
    }
    
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty())
        {
            UIManager.MyInstance.ShowTooltip(transform.position,item.GetDescription());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
