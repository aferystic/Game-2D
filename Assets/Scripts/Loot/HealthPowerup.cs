using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&&!other.isTrigger)
        {
            HealthPotion healthPotion = (HealthPotion)Instantiate(Resources.Load("HealthPotion"));
            backpack.AddItem(healthPotion);
            Destroy(this.gameObject);
        }
    }
}
