using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    private Backpack backpack;
    private KeyCode OpenBackpack;

    [SerializeField]
    private QuestLog questLog;
    private KeyCode OpenQuestLog;

    [SerializeField]
    private GameObject toolTip;
    private Text toolTipText;

    [SerializeField]
    private CharacterPanel characterPanel;

    [SerializeField]
    private GameObject Controls_textbox;
    // Start is called before the first frame update
    void Start()
    {
        toolTipText = toolTip.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            backpack.OpenClose();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            questLog.OpenClose();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            characterPanel.OpenClose();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Controls_textbox.active)
                Controls_textbox.SetActive(false);
            else Controls_textbox.SetActive(true);
        }
    }

    public void ShowTooltip(Vector3 position,string description)
    {
        toolTip.transform.position = position;
        toolTipText.text = description;
        toolTip.SetActive(true);
    }
    public void HideTooltip()
    {
        toolTip.SetActive(false);
    }
}
