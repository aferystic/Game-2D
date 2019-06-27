using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Window window;


    public bool isInteracting;
    
    public virtual void Interact()
    {
        
        if (!isInteracting)
        {
            isInteracting = true;
            window.Open(this);
        }
    }

    public virtual void StopInteract()
    {
        
        if (isInteracting)
        {
            isInteracting = false;
            window.Close();
        }

    }
}
