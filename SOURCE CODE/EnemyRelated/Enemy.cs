using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //holds the number of dead bots
    //AKA player kills
    public static int deadBots;

    //enemy variables to use
    protected CharacterController charController;
    protected int health;
    protected int Damage;
    protected float enemyGravity = -1f;

    //Holds the colors to use to
    //indicate bot was hit
    private Color flashColor = Color.red;
    protected Color originalColor;

    private string newBundle;
    private string newAssetName;

    protected Animator dmgAnim;

    public GameObject healthPickup;

    //Holds health pickup object
    // public GameObject healthPickup;

    //our json object used to read json file
    jsonClass jsonObject = new jsonClass();

    //this coroutine handles the enemies flashing
    //red when a bullet hits them as well
    //as turning the enemies back to their original color
    //DONE ITERATIVELY because only two levels of children.
    protected IEnumerator changeEnemyColor()
    {

        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
               // child.GetComponent<Renderer>().material.color = flashColor;
            }
            else
            {
                foreach (Transform childInner in child)
                {
                    if (childInner.gameObject.name != ("HealthTrigger"))
                    {
                    //    childInner.GetComponent<Renderer>().material.color = flashColor;
                    }
                }
            }
        }

        yield return new WaitForSeconds(.3f);

        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                //child.GetComponent<Renderer>().material.color = originalColor;
            }
            else
            {
                foreach (Transform childInner in child)
                {
                    if (childInner.gameObject.name != ("HealthTrigger"))
                    {
                      //  childInner.GetComponent<Renderer>().material.color = originalColor;
                    }
                }
            }
        }

    }

    //This function generates a random number
    //and depending on the number a health
    //pickup will be dropped
    protected bool shouldDropHealth()
    {
        int randomInt = UnityEngine.Random.Range(1, 20);

        if(randomInt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected IEnumerator getHealthGameObj()
    {
        //Change the bundle and asset name to load the correct asset from the correct place
        newBundle = "healthbundle";
        newAssetName = "HealthPickup";

        //overwrite JSON
        jsonObject = jsonScript.overWriteJSON(jsonObject, newBundle, newAssetName);

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
            
            //make a health pickup and destroy the bot
            GameObject health = Instantiate(bundle.LoadAsset(jsonObject.assetName)) as GameObject;
            health.transform.position = transform.position + new Vector3(0,2,0);
            Destroy(gameObject);    

            // Unload stuff to save memory
            bundle.Unload(false);

            //free memory from web stream?
            www.Dispose();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //decrement health until it reaches 0
        //and make the bot "flash red"
        if (col.gameObject.name == "Bullet(Clone)" || col.gameObject.name == "Bullet")
        {
            dmgAnim.SetBool("hit", true);
            health -= PlayerBullet.getBulletDamage();
          //  StartCoroutine(base.changeEnemyColor());
            StartCoroutine(leaveDmgAnim());
        }
    }

    IEnumerator leaveDmgAnim()
    {
        yield return new WaitForSeconds(.5f);
        dmgAnim.SetBool("hit", false);
    }
}