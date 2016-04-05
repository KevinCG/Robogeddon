using UnityEngine;
using System;
using System.Collections;

public class CharacterShoot : MonoBehaviour
{
    //used for the bullet object pool
    private GameObject bullet;
    Transform[] bulletPool;

    //used to keep track of how many updates
    //and to know when to spawn a bullet
    private int updateCounter = 0;

    //our json object used to read json file
    jsonClass jsonObject = new jsonClass();

    void Start()
    {
        StartCoroutine(makeAmmoPool());   
    }

    //use late update here instead of update because
    //using update causes bullets to spawn slightly
    //left/right or forward/behind spawner
	void LateUpdate()
    {
        //if player is holding down shoot
        //make bullets and give them proper position and rotation.
	    if(Input.GetMouseButton(0))
        {
            //limit bullets from spawning too fast
            if (updateCounter % 5 == 0) 
            {
                //go through and find an inactive bullet
                for(int i = 0; i < bulletPool.Length; i++)
                {
                    //if we find a bullet that is not active
                    if(!bulletPool[i].gameObject.activeInHierarchy)
                    {
                        //Make sure the bullet is not moving or spinning
                        bulletPool[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        bulletPool[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                        //assign the position and rotation of the bullet
                        bulletPool[i].rotation = transform.rotation;
                        bulletPool[i].position = transform.position + transform.up * .8f;
                        
                        //make it active in the scene
                        bulletPool[i].gameObject.SetActive(true);

                        //stop looking for a bullet
                        //we found one that was available
                        break;
                    }
                }
            }
        }

        //increment update counter
        updateCounter++;
	}

    private IEnumerator makeAmmoPool()
    {
        //will hold the bundle name we need
        string newBundle = "ammobundle";

        //will hold the name of the asset
        string newAssetName = "Bullet";

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

            //make a new bullet
            bullet = Instantiate(bundle.LoadAsset(jsonObject.assetName)) as GameObject;
            
            //get rid of double (Clone) in name
            bullet.name = jsonObject.assetName;

            // Unload stuff to save memory
            bundle.Unload(false);

            //free memory from web stream?
            www.Dispose();
        }

        //initialize the bullet object pool
        bulletPool = new Transform[500];
        Transform bulletTransform = bullet.transform;
        //setup the bullet object pool
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bulletTransform) as Transform;
            bulletPool[i].gameObject.SetActive(false);
        }
    }
}
