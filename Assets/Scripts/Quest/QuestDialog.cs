using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialog : Window
{
    //private CanvasGroup canvasGroup;

    private static QuestDialog instance;

    [SerializeField]
    private GameObject backBtn, acceptBtn, questDescription, completeBtn, thxBtn;

    private QuestGiver questGiver;

    [SerializeField]
    private Text TextQuestDesc;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questArea;

    private List<GameObject> quests = new List<GameObject>();

    private Quest selectedQuest;

    [SerializeField]
    private Scrollbar myScrollBar;

    public static QuestDialog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestDialog>();
            }
            return instance;
        }

    }


    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuests(QuestGiver questGiver)
    {
        
        this.questGiver = questGiver;
        TextQuestDesc.text = this.questGiver.name + ":";
        foreach(GameObject go in quests)
        {
            Destroy(go);
        }
        questArea.gameObject.SetActive(true);
        questDescription.SetActive(false);
        bool isActive = false;
        foreach (Quest quest in questGiver.MyQuests)
        {
            if (quest != null && quest.IsActive)
            {
                isActive = true;
                GameObject go = Instantiate(questPrefab, questArea);
                go.GetComponent<Text>().text = quest.MyTitle;
                
                go.GetComponent<QGQuestScript>().MyQuest = quest;

                quests.Add(go);

                if (QuestLog.MyInstance.HasQuest(quest) && quest.isComplete)
                {
                    go.GetComponent<Text>().text += " (C)";
                }
                else if (QuestLog.MyInstance.HasQuest(quest))
                {
                    Color c = go.GetComponent<Text>().color;
                    c.a = 0.5f;
                    go.GetComponent<Text>().color = c;
                }
            }

        }
        if (!isActive)
        {
            GameObject go = Instantiate(questPrefab, questArea);
            go.GetComponent<Text>().text = "I have no quests for You";
            quests.Add(go);
        }

    }

    public override void Open(NPC npc)
    {
        //if ((npc as QuestGiver).IsActive)
        myScrollBar.value = 1.0f;
        canvasGroup.alpha = 1;
        ShowQuests(npc as QuestGiver);
        base.Open(npc);
        

    }
    /*
    public void Close()
    {
        canvasGroup.alpha = 0;
    }*/

    public void ShowQuestInfo(Quest quest)
    {
        if ( quest != null && quest.IsActive)
        {
            selectedQuest = quest;
            if (quest.IsObjectivesToCollect == false)
            {
                thxBtn.SetActive(true);
            }
            else
            {
                if (QuestLog.MyInstance.HasQuest(quest) && quest.isComplete)
                {
                    acceptBtn.SetActive(false);
                    completeBtn.SetActive(true);
                    quest.ReceivePrize = true;
                }
                else if (!QuestLog.MyInstance.HasQuest(quest))
                {
                    acceptBtn.SetActive(true);
                }
            }
                backBtn.SetActive(true);
                questArea.gameObject.SetActive(false);
                questDescription.SetActive(true);

            if (quest.MyTitle == "Final" && !quest.isComplete)
            {
                questDescription.GetComponent<Text>().text = string.Format("<b>Bring me my daughter, please!</b>");
            }
            else
                questDescription.GetComponent<Text>().text = string.Format("<b>{0}\n</b><size=9>{1}</size>", quest.MyTitle, quest.MyDescription);
        }
    }

    public void Back()
    {
        backBtn.SetActive(false);
        acceptBtn.SetActive(false);
        ShowQuests(questGiver);
        completeBtn.SetActive(false);
        thxBtn.SetActive(false);
        myScrollBar.value = 1.0f;
    }

    public void Accept()
    {
        QuestLog.MyInstance.AcceptQuest(selectedQuest);
        Back();
    }

    public override void Close()
    {
        completeBtn.SetActive(false);
        base.Close();
    }

    public void Thx()
    {
        Accept();
        CompleteQuest();
    }
    public void CompleteQuest()
    {
        if (selectedQuest.isComplete)
        {
            for (int i=0;i<questGiver.MyQuests.Length;i++)
            {
                if (selectedQuest == questGiver.MyQuests[i])
                {
                    questGiver.MyQuests[i] = null; //remove quest
                }
            }

            foreach (CollectObjective o in selectedQuest.CollectObjectives)
            {
                Backpack.MyInstance.itemCountChangedEvent -= new ItemCountChanged(o.UpdateItemCount);
                o.UpdateItemCount();
                o.Complete();
            }

            foreach (KillObjective o in selectedQuest.MyKillObjectives)
            {
                Character.MyInstance.killConfirmedEvent -= new KillConfirmed(o.UpdateKillCount);
            }
            Player.myInstance.gainExperience(selectedQuest.MyXp);
            if (selectedQuest.MyReward != null)
            foreach (Object o in selectedQuest.MyReward)
                {
                    if (o is HealthPotion)
                    {
                        HealthPotion hp = (HealthPotion)Instantiate(Resources.Load("HealthPotion"));
                        Backpack.MyInstance.AddItem(hp);
                    }
                    else
                    Backpack.MyInstance.AddItem(o as Item);
                }

            if (selectedQuest.Next != "")
            {
                questGiver.activeQuest(selectedQuest.Next);
            }
            if (selectedQuest.MyTitle== "Getting ready for battle.")
                BackgroundMusic.MyInstance.setToFight();
            if (selectedQuest.MyTitle == "Final")
                FindObjectOfType<GameManager>().GameWin();
            
            QuestLog.MyInstance.RemoveQuest(selectedQuest.MyQuestScript);


            Back();
        }
    }
}
