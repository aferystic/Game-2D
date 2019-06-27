using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private Player parent;

    private void Start()
    {
        parent = GetComponentInParent<Player>();
    }

    private IInteractable[] attack;//reference to interface IInteractable

    public void EndAttack(IInteractable enemy)
    {
        //parent.isAttacked = false;
        if (attack != null)
        {
            int length = attack.Length;
            for (int i = 0; i < attack.Length; i++)
            {
                if (attack[i] == enemy)
                {
                    attack[i].StopInteract();
                    IInteractable[] attackBuffer = new IInteractable[length - 1];
                    int buffer = 0;
                    for (int j = 0; j < length; j++)
                    {
                        if (j != i)
                        {
                            attackBuffer[j - buffer] = attack[j];
                        }
                        else
                        {
                            buffer = 1;
                        }
                    }
                    attack = attackBuffer;
                }
            }
        }
    }

    public void CheckAttack()
    {


        if (attack != null && parent.isAttacking)
        {
            for (int i = 0; i < attack.Length; i++)
                if (attack[i] != null)
                {
                    attack[i].Interact();
                }
        }
    }

            
            

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Enemy")
        {
            IInteractable enemy = collision.GetComponent<IInteractable>();
            if (attack != null)
            {
                int length = attack.Length;

                IInteractable[] attackBuffer = new IInteractable[length + 1];
                for (int j = 0; j < length; j++)
                {
                    attackBuffer[j] = attack[j];
                }
                attackBuffer[length] = enemy;
                attack = attackBuffer;
            }
            else
            {
                attack = new IInteractable[1];
                attack[0] = enemy;
            }
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy")
        {
            if (attack != null)
            {
                IInteractable enemy = collision.GetComponent<IInteractable>();
                int length = attack.Length;
                for (int i = 0; i < attack.Length; i++)
                {
                    if (attack[i] == enemy)
                    {
                        attack[i].StopInteract();
                        IInteractable[] attackBuffer = new IInteractable[length - 1];
                        int buffer = 0;
                        for (int j = 0; j < length; j++)
                        {                            
                            if (j != i)
                            {
                                attackBuffer[j - buffer] = attack[j];
                            }
                            else
                            {
                                buffer = 1;
                            }
                        }
                        attack = attackBuffer;
                    }
                }

            }

        }
    }

}
