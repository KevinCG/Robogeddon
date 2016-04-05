using UnityEngine;
using System.Collections;

public class fatBot : Enemy {

    //used for storing the agent and
    //finding/following the player
    private Transform player;
    private NavMeshAgent agent;
   // private Animator dmgAnim;

    private bool shouldAddKill = true;

    private Renderer enemyRenderer;
  //  private Color originalColor;
    

    void Start()
    {
        dmgAnim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        //set the original target destination of the bot
        player = GameObject.Find("Player").transform; 
        agent = GetComponent<NavMeshAgent>(); 
        agent.destination = player.position;

        //set the bot's health
        health = 100;

        //set the original color of the bot
        //originalColor = transform.GetChild(0).GetComponent<Renderer>().material.color;  
    }


    void Update()
    {
        //if the bot should be dead
        //add a player kill
        //and destroy the bot
        if (health <= 0 && shouldAddKill)
        {
            //   Destroy(gameObject); //PUT BACK FOR DEMO

            Enemy.deadBots++;
            shouldAddKill = false;
            Destroy(gameObject);
            //if this bot should drop a health pickup
            //create the health pickup at the bots position
            if (base.shouldDropHealth())
            {
               //GameObject healthObj = Instantiate(healthPickup) as GameObject; //PUT BACK FOR DEMO
               //healthObj.transform.position = transform.position + new Vector3(0, 2, 0); //PUT BACK FOR DEMO
               
                StartCoroutine(getHealthGameObj());
            }
        }
        else
        {
            //update the target destination of the bot
            agent.destination = player.position;
        }

    }

   /* void OnCollisionEnter(Collision col)
    {
        //decrement health until it reaches 0
        //and make the bot "flash red"
        if (col.gameObject.name == "Bullet(Clone)")
        {
            dmgAnim.SetBool("hit", true);
            
            health -= PlayerBullet.getBulletDamage();
            StartCoroutine(base.changeEnemyColor());
            StartCoroutine(leaveDmgAnim());
        }
    }*/

    public Vector3 getNavAgentVelocity()
    {
        return agent.velocity;
    }

   /* IEnumerator leaveDmgAnim()
    {
        yield return new WaitForSeconds(.5f);
        dmgAnim.SetBool("hit", false);
    }
    */
}
