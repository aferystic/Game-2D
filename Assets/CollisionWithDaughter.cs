using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithDaughter : MonoBehaviour
{
    public static bool isCollision = false;
    private void Awake()
    {
    }
    void OnTriggerEnter2D(Collider2D daughter)
    {
        if (daughter.name == "FishermanDaughter")
        {
            isCollision = true;
            QuestLog.MyInstance.CheckCompletion();
            BackgroundMusic.MyInstance.setToCorka();
            GameObject fishermansDaughter = GameObject.Find("FishermanDaughter") ;

            fishermansDaughter.GetComponent<Rigidbody2D>().mass = 500;
            fishermansDaughter.GetComponent<Animator>().SetFloat("moveX", (float)-0.0);
            fishermansDaughter.GetComponent<Animator>().SetFloat("moveY", (float)-1.0);
            fishermansDaughter.GetComponent<Animator>().SetBool("moving", false);
            Destroy(fishermansDaughter.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit2D(Collider2D daughter)
    {
        if (daughter.name == "FishermanDaughter")
        {

            isCollision = false;
            QuestLog.MyInstance.CheckCompletion();

        }
    }

}
