using UnityEngine;
using System;
using System.Collections;

public class tallBot : Enemy
{
    //used for storing the agent and
    //finding/following the player
    private Transform player;
    private NavMeshAgent agent;

    private bool shouldAddKill = true;

    //our json object used to read json file
   // jsonClass jsonObject = new jsonClass();

    void Start()
    {
        dmgAnim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();

        //set the original target destination of the bot
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;

        //set the bot health
        health = 45;

        //set the original color of the bot
      //  originalColor = transform.GetChild(0).GetComponent<Renderer>().material.color;

    }

    void Update()
    {
        //if the bot should be dead
        //add a player kill
        //and destroy the bot
        if (health <= 0 && shouldAddKill)
        {
            Enemy.deadBots++;
            shouldAddKill = false;
            Destroy(gameObject);
            //if this bot should drop a health pickup
            //create the health pickup at the bots position
            if (base.shouldDropHealth())
            {
               // GameObject healthObj = Instantiate(healthPickup) as GameObject; //PUT BACK FOR DEMO
               // healthObj.transform.position = transform.position + new Vector3(0, 2, 0); //PUT BACK FOR DEMO
                
                    StartCoroutine(getHealthGameObj());
            }

        }
        else
        {
            //update the target destination of the bot
            agent.destination = player.position;
        }
    }

}
