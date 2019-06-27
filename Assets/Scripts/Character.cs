using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void KillConfirmed(Character character);

public class Character : MonoBehaviour
{
    public event KillConfirmed killConfirmedEvent;

    protected Animator animator;
    [SerializeField]
    protected float speed;
    public float MySpeed
    {
        get => speed;
        set { speed = value; }
    }
    protected Vector2 direction;

    [SerializeField]
    private string type;

    public bool isAttacking = false;

    private static Character instance;

    public static Character MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Character>();
            }
            return instance;
        }

    }

    public string MyType { get => type;}

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

   

    public void OnKillConfirmed(Character character)
    {
        if (killConfirmedEvent != null)
        {
            killConfirmedEvent(character);
        }
    }
}
