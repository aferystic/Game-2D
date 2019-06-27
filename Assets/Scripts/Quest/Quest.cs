using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives;

    [SerializeField]
    private KillObjective[] killObjectives;
    
    public QuestScript MyQuestScript { get; set; }

    public string MyTitle { get => title; set => title = value; }
    public string MyDescription { get => description; set => description = value; }

    public CollectObjective[] CollectObjectives { get => collectObjectives;}

    [SerializeField]
    private bool isActive = false;

    public bool IsActive { get => isActive; set => isActive = value; }

    [SerializeField]
    private string nextQuest;

    [SerializeField]
    private int xp;

    [SerializeField]
    private Object[] reward;

    private bool isObjectivesToCollect;
    public bool IsObjectivesToCollect { get => isObjectivesToCollect; set => isObjectivesToCollect = value; }


    //[SerializeField]
    // private int rewardCount=1;

    public bool isComplete
    {
        get
        {

            foreach (Objective o in collectObjectives)
            {

                if (!o.isComplete)
                {
                    return false;
                }
            }
            foreach (Objective o in killObjectives)
            {

                if (!o.isComplete)
                {
                    return false;
                }
            }
            if (title == "Final")
            {
                if (!CollisionWithDaughter.isCollision)
                {
                    return false;
                }
            }
            return true;
        }
    }

    private bool receivePrize;



    public QuestGiver MyQuestGiver { get; set; }
    

    public KillObjective[] MyKillObjectives { get => killObjectives; set => killObjectives = value; }
    public bool ReceivePrize { get => receivePrize; set => receivePrize = value; }
    public int MyXp { get => xp;}
    public string Next { get => nextQuest;}
    public Object[] MyReward { get => reward; }
    //public int MyRewardCount { get => rewardCount; }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount { get => amount;}
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }
    public string MyType { get => type; set => type = value; }

    public bool isComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    
    public void UpdateItemCount(Item item)
    {
        if(MyType.ToLower() == item.GetItemName().ToLower())
        {
            MyCurrentAmount = Backpack.MyInstance.GetItemCount(item.GetItemName());
            QuestLog.MyInstance.CheckCompletion();
            QuestLog.MyInstance.UpdateSelected();
        }
        //QuestLog.MyInstance.CheckCompletion();

    }

    public void UpdateItemCount()
    {
        
            MyCurrentAmount = Backpack.MyInstance.GetItemCount(MyType);
            QuestLog.MyInstance.CheckCompletion();
            QuestLog.MyInstance.UpdateSelected();
        

        //QuestLog.MyInstance.CheckCompletion();

    }

    public void Complete()
    {
        
        Backpack.MyInstance.RemoveItem(MyType, MyAmount);
    }
}


[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(Character character)
    {
        if (MyType == character.MyType)
        {
            MyCurrentAmount++;

            QuestLog.MyInstance.CheckCompletion();
            QuestLog.MyInstance.UpdateSelected();
        }
    }
}