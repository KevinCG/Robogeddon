using UnityEngine;
using System.Collections;

public class ballBot : Enemy
{
    //used for storing the agent and
    //finding/following the player
    private Transform player;
    private NavMeshAgent agent;

	void Start ()
    {
        //set the original target destination of the bot
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
       
        //set the bot's health
        health = 25;

        //set the original color of the bot
        originalColor = transform.GetChild(0).GetComponent<Renderer>().material.color;

    }
	
	void Update ()
    {
        //if the bot should be dead
        //add a player kill
        //and destroy the bot
        if (health <= 0)
        {
            Enemy.deadBots++;

            Destroy(gameObject);

            //if this bot should drop a health pickup
            //create the health pickup at the bots position
            if (base.shouldDropHealth())
            {
                GameObject healthObj = Instantiate(healthPickup) as GameObject;
                healthObj.transform.position = transform.position + new Vector3(0, 2, 0);
            }
        }
        else
        {
            //update the target destination of the bot
            agent.destination = player.position;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //decrement health until it reaches 0
        //and make the bot "flash red"
        if (col.gameObject.name == "Bullet(Clone)")
        {
            health -= PlayerBullet.getBulletDamage();
            StartCoroutine(base.changeEnemyColor());
        }    
    }
}
