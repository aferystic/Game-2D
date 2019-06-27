using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverNPCInteract : NPC, IInteractable
{
    [SerializeField]
    private Player player;

    private bool isOpen = false;
    
    public override void Interact()
    {
       //QuestDialog.MyInstance.Open();
       // isOpen = true;

    }

    public override void StopInteract()
    {
        QuestDialog.MyInstance.Close();
        //isOpen = false;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }
    
    private void ClickTarget()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           // RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);
            //if (hit.collider != null && hit.collider.tag == "Interactable")
            
                
                if (isInteracting)
                {
                    player.StopInteract();
                    //isInteracting = false; 
                } 
                else
                {
                    player.Interact();
                    //isInteracting = true;
            }
                
                
                
            

        }

    }

}
