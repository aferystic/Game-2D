using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : NPC
{
    [SerializeField]
    private Quest[] quests;

    public Quest[] MyQuests { get => quests; set => quests = value; }
    

    [SerializeField]
    private Sprite question, exclamationSilver, exclamation;

    [SerializeField]
    private SpriteRenderer statusRenderer;
    

    

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.MyQuestGiver = this;
            if (quest.CollectObjectives.Length != 0 || quest.MyKillObjectives.Length != 0)
                quest.IsObjectivesToCollect = true;
            else
                quest.IsObjectivesToCollect = false;

            if (quest.IsActive)
            {
                statusRenderer.sprite = question;
            }
            //Debug.Log(quest.MyTitle + "= " + quest.IsObjectivesToCollect);
        }
    }

    public void activeQuest(string title)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();
        int c = 0;
        foreach (QuestGiver qg in questGivers)
        {
            c = 0;
                foreach (Quest q in qg.quests)
                {
                    if (q != null && q.MyTitle == title)
                    {
                        qg.MyQuests[c].IsActive = true;
                        qg.UpdateQuestStatus();

                        if (q.MyTitle == "Final")
                        {
                            QuestLog.MyInstance.AcceptQuest(q);

                        }

                        break;
                    }
                    c++;
                }
            
        }
    }

    public void UpdateQuestStatus()
    {
        int count = 0;
        bool isQuest = false;
        foreach (Quest quest in quests)
        {
            if (quest != null && quest.IsActive)
            {
                if (quest.isComplete && QuestLog.MyInstance.HasQuest(quest)) //one (or more) quest is finished
                {
                    statusRenderer.sprite = exclamation;
                    break;
                }
                else if (!QuestLog.MyInstance.HasQuest(quest)) //quest giver has quest 
                {
                    statusRenderer.sprite = question;
                    isQuest = true;
                }
                else if (!quest.isComplete && QuestLog.MyInstance.HasQuest(quest)) //quest giver hasnt quest but not all quests are completed
                {
                    if (!isQuest)
                        statusRenderer.sprite = exclamationSilver;
                }
            }
            else
                count++;
        }

        if (count == quests.Length) //all quest has been done
        {
            statusRenderer.sprite = null;
        }

    }

}
