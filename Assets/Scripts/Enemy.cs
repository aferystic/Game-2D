using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IInteractable
{


    private Stat health;
    [SerializeField]
    private float health_value;
    [SerializeField]
    public float damage;
    //private float maxHealth = 100;
    // [SerializeField]
    // private Stat mana;
    // private float maxMana = 80; //poźniej można tak zrobić jak będą grafiki by wyswietlać staty

    // private float health = 30;

    private int changeDirection = 0;
    private int randomNumber = 0;
    private int frame = 0;

    [SerializeField]
    private Loot[] loot;

    public Transform target { get; set; }

    private Vector2 directionAnimation;


    public void Interact()
    {
        health.myCurrentValue -= Player.myInstance.getDamageRoll();
        if (health.myCurrentValue <= 0)
        {
            if (this.MyType == "Minotaur")
            {
                BackgroundMusic.MyInstance.setToNormal();
            }
            MakeLoot();
            //Player.myInstance.isAttacked = false;
            Player.myInstance.damage -= damage;
            //Player.myInstance.StopAttack();
            Player.myInstance.attackCollision.EndAttack(this);
            this.gameObject.SetActive(false);
            Character.MyInstance.OnKillConfirmed(this);
            Destroy(this);
        }
    }

    public void StopInteract()
    {

    }

    public void MakeLoot()
    {
        int length = loot.Length;
        if (length > 0)
        {
            int rand = UnityEngine.Random.Range(0, length);

            if (loot[rand] != null)
            {
                GameObject gameObjectBuffer = Instantiate(loot[rand].gameObject, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                gameObjectBuffer.SetActive(true);
            }
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        (health = new Stat()).Init(health_value);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        FollowTarget();
        if (frame >= 10)
        {
            settingDirection();
            animateMovement(directionAnimation);
            frame = 0;
        }
        else
        {
            frame++;
        }
        base.Update();
    }

    private void settingDirection()
    {
        directionAnimation = Vector2.zero;
        if(target!=null)
        {
            if (Math.Abs(target.position.x - transform.position.x) > Math.Abs(target.position.y - transform.position.y))
            {
                if ((target.position.x - transform.position.x) > 0)//w prawo 
                {
                    directionAnimation += Vector2.right;
                }
                else//w lewo
                {
                    directionAnimation += Vector2.left;
                }
            }
            else
            {
                if ((target.position.y - transform.position.y) > 0)//do góry
                {
                    directionAnimation += Vector2.up;
                }
                else//na dół
                {
                    directionAnimation += Vector2.down;
                }
            }      
            
        }        
    }

    private void FollowTarget()
    {
        if(target!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
           // animator.SetBool("moving",true);
        }
        else
        {
            //animator.SetBool("moving", false);
            //MovementRandom();
        }
    }

    public void MovementRandom()
    {
        changeDirection++;
        if (changeDirection % 50 ==0)
        {
            System.Random rand = new System.Random((int)DateTime.Now.Ticks);
            randomNumber = rand.Next(4);
        }
        direction = Vector2.zero;
        
        switch (randomNumber)
        {
            case 0:
                direction += Vector2.left;
                break;
            case 1:
                direction += Vector2.right;
                break;
            case 2:
                direction += Vector2.down;
                break;
            case 3:
                direction += Vector2.up;
                break;
        }
    }

    public void animateMovement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);                 
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
