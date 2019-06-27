﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool markedComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select()
    {
            GetComponent<Text>().color = Color.red;
            QuestLog.MyInstance.ShowDescription(MyQuest);
        
        
    }
    public void DeSelect()
    {
        GetComponent<Text>().color = Color.black;

    }
    public void isComplete()
    {
        if (MyQuest.isComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponent<Text>().text += " (COMPLETE)"; 

        } else if (!MyQuest.isComplete)
        {
            markedComplete = false;
            GetComponent<Text>().text = MyQuest.MyTitle;
        }
    }
}
