using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{

    protected Backpack backpack;

    protected KeyCode addItem;

    protected bool collision;

   // protected Backpack backpack;
    // Start is called before the first frame update

    protected virtual void Start()
    {
        addItem = KeyCode.E;
        backpack = Backpack.MyInstance;
        collision = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void AddItem()
    {
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            collision = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            collision = false;
        }
    }
}
