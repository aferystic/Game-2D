using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : Stat
{
    private Text levelText;
    private BarStat experience;

    public float myCurrentValue
    {
        get => currentValue;
        set {currentValue = value; }
    }
    private void writeLevel()
    {
        levelText.text = "Lv." + this.myCurrentValue;
    }
    public void init(BarStat exp)
    {
        levelText = GetComponent<Text>();
        minValue = 1;
        myCurrentValue = 1;
        maxValue = 99;
        experience = exp;
        setExperienceAmount();
        writeLevel();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setExperienceAmount()
    {
        experience.myCurrentValue = 0;
        experience.myMaxValue = this.myCurrentValue * 10;
    }
    public void gainExperience(float exp)
    {
        experience.increaseValue(exp);
        while (experience.myCurrentValue >= experience.myMaxValue)
        {

            levelUp();
            experience.increaseValue(experience.excessValue);
            writeLevel();
        }
    }
    void levelUp()
    {
        this.increaseValue(1);
        Player.myInstance.myStrength.myCurrentValue += 1;
        Player.myInstance.myAgility.myCurrentValue += 1;
        Player.myInstance.myStamina.myCurrentValue += 1;
        Player.myInstance.myHealth.myMaxValue = Player.myInstance.myStamina.myCurrentValue * 5;
        Player.myInstance.myHealth.myCurrentValue = Player.myInstance.myHealth.myMaxValue;
        CharacterPanel.MyInstance.updateStats();
      //  Player.myInstance.myHealth.myCurrentValue = Player.myInstance.myHealth.myMaxValue;

        setExperienceAmount();
    }
}
