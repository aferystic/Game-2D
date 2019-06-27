using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    CharacterPanel characterPanel;
    private static Player Instance;
    public static Player myInstance
        {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<Player>();
            }
            return Instance;
        }
    }
    [SerializeField]
    private BarStat health;
    public BarStat myHealth
    {
        get => health;
        set { health = value; }
    }
    [SerializeField]
    private BarStat mana;
    public BarStat myMana
    {
        get => mana;
        set { mana = value; }
    }
    [SerializeField]
    private BarStat experience;
    [SerializeField]
    private Level level;
    [SerializeField]
    public AttackCollision attackCollision;

    private Stat strength;
    public Stat myStrength
    {
        get => strength;
        set { strength = value; }
    }
    private Stat agility;
    public Stat myAgility
    {
        get => agility;
        set { agility = value; }
    }
    private Stat stamina;
    public Stat myStamina
    {
        get => stamina;
        set {
            stamina = value;
            health.myMaxValue = stamina.myCurrentValue * 5;
        }
    }
    private float minDamage;
    public float myMinDamage
    {
        get
        {
           return characterPanel.getWeaponMinDamage()+myStrength.myCurrentValue;
        }
        set { minDamage = value; }
    }
    private float maxDamage;
    public float myMaxDamage
    {
        get
        {
            return characterPanel.getWeaponMaxDamage() + myStrength.myCurrentValue;

        }
        set { maxDamage = value; }
    }
    private float maxMana = 80;
    //public bool isAttacked = false;
    //public bool attackEnemy = false;
    private float regeneration = 0.1F;
    public float damage = 0.0F;
    private Vector3 min, max;
    private int frame = 0;
    private int frameAtacked = 0;

    private IInteractable interactable;//reference to interface IInteractable

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        (strength = new Stat()).Init(1, 1000);
        (agility = new Stat()).Init(1, 1000);
        (stamina = new Stat()).Init(10, 1000);
        //health.Init(myStamina.myCurrentValue*5);
        health.Init(50);
        mana.Init(maxMana);
   
        minDamage = 2;
        maxDamage = 3;
        characterPanel = CharacterPanel.MyInstance;
        //strength.Init(5);
        //agility.Init(5);
        //stamina.Init(10);
        //animator = GetComponent<Animator>();
        //level.myCurrentValue
        level.init(experience);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //attackEnemy = false;
        GetInput();        
        AnimateMovement(direction);
        //if (isAttacked)
        //{
        if (frameAtacked < 30)
        {
            frameAtacked++;
        }
        else
        {
            health.reduceValue(damage);
            if (health.myCurrentValue <= 0)
            {
                if (BackgroundMusic.MyInstance.getAudioClip()!="GameOver")
                    BackgroundMusic.MyInstance.setToGameOver();
                FindObjectOfType<GameManager>().GameOver();
            }
            frameAtacked = 0;
        }


        //}
        //else
       // {
            //health.increaseValue(regeneration);
        //}
        if(isAttacking)
        {
            if (frame == 0)
            {
                attackCollision.CheckAttack();
                frame++;
            }
            else if (frame < 30)
            {
                frame++;
            }
            else
            {
                frame = 0;
                StopAttack();
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
         Move();
        base.Update();

    }

    public void ChangeHealth(float change)
    {
        if (change > 0)
            this.health.increaseValue(change);
        else this.health.reduceValue(change);
    }
    void GetInput()
    {
        direction = Vector2.zero;
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A))) { direction += Vector2.left;}
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D))) { direction += Vector2.right;}
        if ((Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.W))) { direction += Vector2.up;}
        if ((Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.S))) { direction += Vector2.down;}
        if (Input.GetKey(KeyCode.Space)) { Attack(); }
    }

    public void AnimateMovement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            if (frame == 0)
            {
                animator.SetBool("moving", true);
            }
            
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }


    private void  Attack()
    {

        if (!isAttacking)
        {
            //attackEnemy = true;
            isAttacking = true;
            animator.SetBool("attack", isAttacking);
          
        }
    }

    public void Interact()
    {
        if (interactable!=null)
        {
            interactable.Interact();
        }
    }

    public void StopInteract()
    {
        if (interactable != null)
        {
            interactable.StopInteract();
        }
    }

    private void Move()
    {
        transform.Translate(translation: direction * speed * Time.deltaTime);
    }

    public void StopAttack()
    {
        isAttacking = false;
        //attackEnemy = false;
        animator.SetBool("attack", isAttacking);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interactable = collision.GetComponent<IInteractable>();
        }
        

    }
    public void gainExperience(float exp)
    {
        level.gainExperience(exp);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (interactable != null)
            {
                interactable.StopInteract();
                interactable = null;
            }
            
        }
        
    }
    public float getDamageRoll()
    {
        float tmp = Random.Range(myMinDamage, myMaxDamage);
        return tmp;
    }

}

