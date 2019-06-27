using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stat : MonoBehaviour
{
    public float excessValue;
    protected float maxValue;
    public float myMaxValue
    {
        get=>maxValue;
        set { maxValue = value; }
    }
    protected float currentValue;
    public float myCurrentValue {
        get => currentValue;
        set {

            if (currentValue > maxValue)
            {
                excessValue = currentValue - maxValue;
                currentValue = (float)Math.Round(maxValue,1);
            }
            else if (currentValue < 0) currentValue = 0;
            else currentValue = value;
        }
    }
    protected float minValue=0;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Init(float maxValue)
    {
        this.myMaxValue = maxValue;
        myCurrentValue = maxValue;
    }
    public void Init(float currValue,float maxValue)
    {
        this.myMaxValue = maxValue;
        myCurrentValue = currValue;
    }

    public void reduceValue(float substracted)
    {
        if (myCurrentValue > minValue)
        {
            myCurrentValue = myCurrentValue - substracted;
            myCurrentValue = (float)Math.Round((double)myCurrentValue, 1);
        }
        else
        {
            myCurrentValue = minValue;
        }
    }

    public void increaseValue(float increment)
    {
        if (myCurrentValue < maxValue)
        {
            myCurrentValue = myCurrentValue + increment;
            myCurrentValue = (float) Math.Round((double)myCurrentValue, 1);
        }
        else
        {
            myCurrentValue = maxValue;
        }
    }
}
