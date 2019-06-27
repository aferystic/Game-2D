using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemCountChanged(Item item);

public class Backpack : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private List<Slot> slots = new List<Slot>();
    // Start is called before the first frame update
    private Image backpackIcon;
    private Text newItemText;
    public event ItemCountChanged itemCountChangedEvent;
    private static Backpack instance;
    public static Backpack MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Backpack>();
            }
            return instance;
        }

    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        slots = GetComponentsInChildren<Slot>().ToList();
        backpackIcon = GameObject.Find("Backpack_icon").GetComponent<Image>();
        newItemText = GameObject.Find("NewItemsText").GetComponent<Text>();
    }
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.transform.position = new Vector3(canvasGroup.transform.position.x + 300, canvasGroup.transform.position.y, canvasGroup.transform.position.z);
            backpackIcon.color = new Color(backpackIcon.color.r, backpackIcon.color.g, backpackIcon.color.b, 1.0f);

        }

        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.transform.position = new Vector3(canvasGroup.transform.position.x - 300, canvasGroup.transform.position.y, canvasGroup.transform.position.z);
            backpackIcon.color = new Color(backpackIcon.color.r, backpackIcon.color.g, backpackIcon.color.b, 0.6f);
            newItemText.text = "";
        }
        }
    public bool AddItem(Item item)
    {

        if (item is StackableItem)
        {

            foreach (Slot slot in slots)
            {                        
                if (!slot.IsEmpty())
                {
                    if (item.GetItemName() == slot.GetItem().GetItemName())
                    {
                        if (slot.AddStackableItem())
                        {
                            onItemCountChanged(item);
                            return true;
                        }
                    }
                }
            }
        }
        foreach(Slot slot in slots)
        {
            if (slot.AddItem(item))
            {
                onItemCountChanged(item);
                if (canvasGroup.alpha == 0)
                {
                    newItemText.text = "!";
                }
                return true;

            }
        }
        return false;
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;
        foreach (Slot slot in slots)
        {
            
            if (!slot.IsEmpty() && slot.GetItem().GetItemName() == type)
            {
                Item item = slot.GetItem();
                itemCount += (item as StackableItem).GetStackQuantity();
            }
        }
        return itemCount;
    }

    public void RemoveItem(string type, int count)
    {
        int tmpCount = 0;
        slots.Reverse(); //remove Item from the highest number of slots
        foreach (Slot slot in slots)
        {

            if (!slot.IsEmpty() && slot.GetItem().GetItemName() == type)
            {
                Item item = slot.GetItem();
                while (tmpCount != count)
                {
                    tmpCount++;
                    if (!slot.RemoveItems(item)) // if false go to the next slot
                        break;
                    
                }
            }
        }
        slots.Reverse(); //back to normal slots possitions
    }

    public void onItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
