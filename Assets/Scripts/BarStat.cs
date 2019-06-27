using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BarStat : Stat
{
    private Image content;
    private Text statText;
    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
        statText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        content.fillAmount = currentValue / maxValue;
        statText.text = currentValue + "/" + maxValue;
    }
}
