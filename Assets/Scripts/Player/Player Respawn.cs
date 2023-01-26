using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRespawn : MonoBehaviour
{
    //used to store last checkpoint.
    private Transform currentCheckpoint;

    //used to reset the players health after respawn.
    private Health playerHealth;

   

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    private void Respawn()
    {

        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
   
}

