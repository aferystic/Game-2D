using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    Player player;
    private static CharacterPanel instance;
    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }
    [SerializeField]
    private Slot helmet, shoulders, chest, gloves, legs, boots, mainWeapon, offhand;
    private CanvasGroup canvasGroup;
    private Image equipmentIcon;
    private Text statValues;
    // Start is called before the first frame update
    void Start()
    {
       player = Player.myInstance;
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        statValues = GetComponentInChildren<Text>();
        equipmentIcon = GameObject.Find("Equipment_icon").GetComponent<Image>();
    }
    //Update is called once per frame
    public void updateStats()
    {
        player.MySpeed = 4 + ((float)0.1 * player.myAgility.myCurrentValue);
        statValues.text = player.myHealth.myMaxValue.ToString() + "\n" +
                          player.myMana.myCurrentValue.ToString() + "\n" +
                          player.myStrength.myCurrentValue.ToString() + "\n" +
                          player.myAgility.myCurrentValue.ToString() + "\n" +
                          player.myStamina.myCurrentValue.ToString() + "\n" +
                          player.myMinDamage+"-"+player.myMaxDamage;
    }
    void Update()
    {
        
    }
    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.transform.position = new Vector3(canvasGroup.transform.position.x - 600, canvasGroup.transform.position.y, canvasGroup.transform.position.z);
            equipmentIcon.color = new Color(equipmentIcon.color.r, equipmentIcon.color.g, equipmentIcon.color.b, 1.0f);
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.transform.position = new Vector3(canvasGroup.transform.position.x + 600, canvasGroup.transform.position.y, canvasGroup.transform.position.z);
            equipmentIcon.color = new Color(equipmentIcon.color.r, equipmentIcon.color.g, equipmentIcon.color.b, 0.6f);
            updateStats();

        }
    }
    public string equipItem(Equipment item)
    {
        string returnText = "equip";
        if (item is Weapon)
            switch ((item as Weapon).myWeaponType)
            {
                case WeaponType.Shield:
                    if (!offhand.IsEmpty()) { returnText = "switch"; }
                    offhand.AddItem(item);
                    break;
                default:
                    if (!mainWeapon.IsEmpty()) { returnText = "switch"; }
                    mainWeapon.AddItem(item);
                    break;
            }
        else
            switch ((item as Armor).myArmorType)
            {
                case ArmorType.Helmet:
                    if (!helmet.IsEmpty())  { returnText = "switch"; }
                    helmet.AddItem(item);
                    break;
                case ArmorType.Shoulders:
                    if (!shoulders.IsEmpty()) { returnText = "switch"; }
                    shoulders.AddItem(item);
                    break;
                case ArmorType.Chest:
                    if (!chest.IsEmpty()) { returnText = "switch"; }
                    chest.AddItem(item);
                    break;
                case ArmorType.Legs:
                    if (!legs.IsEmpty()) { returnText = "switch"; }
                    legs.AddItem(item);
                    break;
                case ArmorType.Gloves:
                    if (!gloves.IsEmpty()) { returnText = "switch"; }
                    gloves.AddItem(item);
                    break;
                case ArmorType.Boots:
                    if (!boots.IsEmpty()) { returnText = "switch"; }
                    boots.AddItem(item);
                    break;
            }
        updateStats();
        return returnText;
    }
    public float getWeaponMinDamage()
    {

        if (mainWeapon.GetItem() != null)
            return (mainWeapon.GetItem() as Weapon).myMinDamage;
        else return 2;

    }
    public float getWeaponMaxDamage()
    {
        if (mainWeapon.GetItem() != null)
            return (mainWeapon.GetItem() as Weapon).myMaxDamage;
        else return 3;

    }
    public void addItemStats(Item item)
    {
        player.myStrength.myCurrentValue += (item as Equipment).myStrength;
        player.myAgility.myCurrentValue += (item as Equipment).myAgillity;
        player.myStamina.myCurrentValue += (item as Equipment).myStamina;
        player.myHealth.myCurrentValue += (item as Equipment).myStamina * 5;
        player.myHealth.myMaxValue = player.myStamina.myCurrentValue * 5;

        updateStats();
    }
    public void subItemStats(Item item)
    {
        player.myStrength.myCurrentValue -= (item as Equipment).myStrength;
        player.myAgility.myCurrentValue -= (item as Equipment).myAgillity;
        player.myStamina.myCurrentValue -= (item as Equipment).myStamina;
        
        player.myHealth.myCurrentValue -= (item as Equipment).myStamina * 5;
        player.myHealth.myMaxValue = player.myStamina.myCurrentValue * 5;
        // if (player.myHealth.myCurrentValue > player.myHealth.myMaxValue)
        //     player.myHealth.myCurrentValue = player.myHealth.myMaxValue;

       // player.myHealth.myCurrentValue = player.myHealth.myMaxValue;
        updateStats();
    }
}
