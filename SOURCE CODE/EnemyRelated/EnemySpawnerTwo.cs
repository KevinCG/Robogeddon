using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class EnemySpawnerTwo : MonoBehaviour
{
    //variables to keep track of time, waves, and how many enemies to spawn
    private float enemiesToSpawn;
    
    //delay between enemy spawns
    private int timeBetweenEnemies = 3;

    //NOT CURRENTLY USED
    private int timeBeforeStart = 15; 
    private int timeBetweenWaves = 45; 

    //wave player is on
    public static int waveNumber;

    //Holds the number of enemies spawned
    private float enemiesSpawned;

    //holds a bool that decides if we are ready
    //to move on to the next wave
    private bool spawningDone = false;

    //our json object used to read json file
    jsonClass jsonObject = new jsonClass();

    private GameObject spawnPoint1;
    private GameObject spawnPoint2;

    void Start()
    {
        //find spawn point one
        spawnPoint1 = GameObject.Find("EnemySpawner1");
        spawnPoint1 = spawnPoint1.transform.GetChild(2).gameObject;

        //find spawn point two
        spawnPoint2 = GameObject.Find("EnemySpawner2");
        spawnPoint2 = spawnPoint2.transform.GetChild(2).gameObject;
        
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
                //will hold the bundle name we need
                string newBundle;

                //will hold the name of the asset
                string newAssetName;

                //this will hold our new bot
                GameObject newBot = null;

                //generate a random number
                int botToSpawn = UnityEngine.Random.Range(1, 4);

                //pick a bot depending on the random number
                switch (botToSpawn)
                {
                    case 1: //Ballbot
                        //Change the bundle and asset name to load the correct asset from the correct place
                        newBundle = "ballbotbundle";
                        newAssetName = "BallBotAnimated";

                        //overwrite JSON
                        jsonObject = jsonScript.overWriteJSON(jsonObject, newBundle, newAssetName);

                        break;

                    case 2: //Tallbot
                        //Change the bundle and asset name to load the correct asset from the correct place
                        newBundle = "tallbotbundle";
                        newAssetName = "TallBotAnimated";

                        //overwrite JSON
                        jsonObject = jsonScript.overWriteJSON(jsonObject, newBundle, newAssetName);

                        break;

                    case 3: //Fatbot
                        //Change the bundle and asset name to load the correct asset from the correct place

                        newBundle = "fatbotbundle";
                        newAssetName = "FatBotAnimated";

                        //overwrite JSON
                        jsonObject = jsonScript.overWriteJSON(jsonObject, newBundle, newAssetName);

                        break;
                }

                //get a new www object with our url
                 using (WWW www = new WWW(jsonObject.URL))
                 {
                     //wait for www to finish download
                     //avoids blocking rest of game
                     yield return www;
                     //if there was an error throw an exception with some info
                     if (www.error != null)
                     {
                         throw new Exception("WWW download had an error:" + www.error);
                     }

                     //get the asset bundle
                     AssetBundle bundle = www.assetBundle;

                    //make a new bot
                     newBot = Instantiate(bundle.LoadAsset(jsonObject.assetName)) as GameObject;

                     // Unload stuff to save memory
                     bundle.Unload(false);

                     //free memory from web stream?
                     www.Dispose();
                 }

                //set its position
                int spawnerToUse = UnityEngine.Random.Range(1, 3);
                switch (spawnerToUse)
                {
                    case 1:
                        newBot.transform.position = spawnPoint1.transform.position;
                        break;
                    case 2:
                        newBot.transform.position = spawnPoint2.transform.position;
                        break;
                }
                //disable and then enable navmesh agent to avoid bad behavior
                // newBot.GetComponent<NavMeshAgent>().enabled = false;
                // newBot.transform.position = transform.position;
                //  newBot.GetComponent<NavMeshAgent>().enabled = true;

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
