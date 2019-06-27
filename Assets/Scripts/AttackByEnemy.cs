using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackByEnemy : MonoBehaviour
{
  

           // private Enemy parentWizard;
        private Player parentPlayer;

    private void Start()
        {
           // parentWizard = GetComponentInParent<Enemy>();
                parentPlayer = GetComponentInParent<Player>();
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                //parentPlayer.isAttacked = true;
                Enemy enemy = collision.GetComponent<Enemy>();
                parentPlayer.damage += enemy.damage;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                //parentPlayer.isAttacked = false;
                Enemy enemy = collision.GetComponent<Enemy>();
                parentPlayer.damage -= enemy.damage;

            }
        }
    }
