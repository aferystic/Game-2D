using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private static QuestLog instance;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

    [SerializeField]
    private Scrollbar listScrollbar;

    [SerializeField]
    private Scrollbar descScrollbar;

    private CanvasGroup canvasGroup;

    private List<QuestScript> questScripts = new List<QuestScript>();

    private List<Quest> quests = new List<Quest>();

    private Image questIcon;

    public Text getShowDesc()
    {
        return questDescription;
    }

    public static QuestLog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void Awake()
    {
        questIcon = GameObject.Find("Quest_icon").GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void AcceptQuest(Quest quest)
    {
        
        foreach (CollectObjective o in quest.CollectObjectives)
        {
            Backpack.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
            o.UpdateItemCount();
        }

        foreach (KillObjective o in quest.MyKillObjectives)
        {
            Character.MyInstance.killConfirmedEvent += new KillConfirmed(o.UpdateKillCount);
        }
        
        quests.Add(quest);
        GameObject go = Instantiate(questPrefab, questParent);
        go.GetComponent<Text>().text = quest.MyTitle;

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;
        questScripts.Add(qs); //so good

        CheckCompletion();



    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }
            
            string receivePrize = "Go to the " + quest.MyQuestGiver.name + " to receive prize";
            selected = quest;
            string objective = getObjective(quest);
            if (quest.MyTitle == "Final")
            {
                objective = quest.MyKillObjectives[0].MyType + ": " + quest.MyKillObjectives[0].MyCurrentAmount + "/" + quest.MyKillObjectives[0].MyAmount + "\n";
                questDescription.text = string.Format("You final task is to kill the almighty Minotaur and bring Fishermans' daughter to her dad.\n{0}", objective);
            }
            else
            {
                if (quest.isComplete)
                {
                    questDescription.text = string.Format("<b>{0}\n</b><size=9>{1}</size>\n\n{2}\n{3}", quest.MyTitle, quest.MyDescription, objective, receivePrize);
                }
                else
                {
                    foreach (Object o in selected.MyReward)
                    {
                        if (selected.MyReward != null)
                            objective += "Reward:" + (o as Item).GetItemName() + "\n";
                    }

                    questDescription.text = string.Format("<b>{0}\n</b><size=9>{1}</size>\n\n{2}", quest.MyTitle, quest.MyDescription, objective);
                }
            }

            
            
        }



    }

    private string getObjective(Quest quest)
    {
        string objective = "";
        foreach (Objective obj in quest.CollectObjectives)
        {
            if (obj.MyCurrentAmount < obj.MyAmount)
                objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            else
                objective += obj.MyType + ": " + obj.MyAmount + "/" + obj.MyAmount + "\n";

        }

        foreach (Objective obj in quest.MyKillObjectives)
        {
            if (obj.MyCurrentAmount < obj.MyAmount)
                objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            else
                objective += obj.MyType + ": " + obj.MyAmount + "/" + obj.MyAmount + "\n";
        }
        return objective;
    }
    

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }
    public void OpenClose()
    {
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            questIcon.color = new Color(questIcon.color.r, questIcon.color.g, questIcon.color.b, 1.0f);
        }
        else
        {
            listScrollbar.value = 1.0f;
            descScrollbar.value = 1.0f;
            canvasGroup.alpha = 1;
            questIcon.color = new Color(questIcon.color.r, questIcon.color.g, questIcon.color.b, 0.6f);
        }
    }


    public void CheckCompletion()
    {
        
        foreach(QuestScript qs in questScripts)
        {
            qs.MyQuest.MyQuestGiver.UpdateQuestStatus();
            qs.isComplete();
        }

    }
    public bool HasQuest(Quest quest)
    {
        return quests.Exists(x => x.MyTitle == quest.MyTitle);  //public bool Exists (Predicate<T> match)
    }
    
    public void RemoveQuest(QuestScript qs)
    {
        questScripts.Remove(qs);
        Destroy(qs.gameObject);
        quests.Remove(qs.MyQuest);
        questDescription.text = string.Empty;
        selected = null;
        qs.MyQuest.MyQuestGiver.UpdateQuestStatus();
        qs = null;
    }
}
