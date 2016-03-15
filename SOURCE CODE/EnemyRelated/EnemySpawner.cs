using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //Different types of robots
    //that can be spawned.
    public GameObject fatBot;
    public GameObject tallBot;
    public GameObject ballBot;

    //Different spawnpoints a bot
    //can spawn at
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;

    //Pickups a bot can drop
    public GameObject healthPickup;

    //variables to keep track of time, waves, and how many enemies to spawn
    private float enemiesToSpawn;
    public int timeBeforeStart = 15; //NOT CURRENTLY USED
    public int timeBetweenEnemies = 3;
    public int timeBetweenWaves = 45; //NOT CURRENTLY USED
    public static int waveNumber;

    //Holds the number of enemies spawned
    private float enemiesSpawned;

    //holds a bool that decides if we are ready
    //to move on to the next wave
    private bool spawningDone = false;

    void Start ()
    {
        //how many enemies to spawn on first wave
        enemiesToSpawn = 10; 
        
        //no enemies spawned yet
        enemiesSpawned = 0;

        //no dead enemies yet
        Enemy.deadBots = 0;

        //start at wave 1
        waveNumber = 1; 

        //This starts the coroutine that will 
        //handle spawning enemies in waves
        StartCoroutine(spawnEnemies());
    }

    void Update()
    {
        //If the player finished the current wave
        //Move on to the next wave
        if (Enemy.deadBots == enemiesSpawned && spawningDone)
        {
            waveNumber++;
            spawningDone = false;
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies()
    {
       // yield return new WaitForSeconds(timeBeforeStart);
        while (true)
        {
            
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                //pick a random robot to spawn
                //three different types to be chosen
                GameObject newBot = null;
                int botToSpawn = Random.Range(1, 4);
               
                switch (botToSpawn)
                {
                    case 1: //ballBot
                        newBot = Instantiate(ballBot) as GameObject;
                        break;
                    case 2: //tallBot
                        newBot = Instantiate(tallBot) as GameObject;
                        break;
                    case 3: //fatBot
                        newBot = Instantiate(fatBot) as GameObject;
                        break;
                }

                //spawn the bot at the spawn point chosen randomly
                int spawnerToUse = Random.Range(1, 3);
                switch (spawnerToUse)
                {
                    case 1:
                        newBot.transform.position = spawnPoint1.transform.position;
                        break;
                    case 2:
                        newBot.transform.position = spawnPoint2.transform.position;
                        break;
                }

                //wait a little before we continue to spawn more bots
                yield return new WaitForSeconds(timeBetweenEnemies);
            }

           //add the enemies spawned the current wave to the total
            enemiesSpawned += enemiesToSpawn;

            //add more enemies to spawn the next wave
            enemiesToSpawn += 10;

            //we finished the current wave
            spawningDone = true;

            //get out of infinite while loop
            break;
        }
        //no need to wait ATM
        yield return new WaitForSeconds(0);
    }
}
