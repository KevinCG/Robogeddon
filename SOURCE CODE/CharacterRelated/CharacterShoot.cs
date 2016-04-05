using UnityEngine;
using System.Collections;

public class CharacterShoot : MonoBehaviour
{
    //used for the bullet object pool
    public GameObject bullet;
    Transform[] bulletPool;

    //used to keep track of how many updates
    //and to know when to spawn a bullet
    private int updateCounter = 0;

    void Start()
    {
        //initialize the bullet object pool
        bulletPool = new Transform[500];

        //setup the bullet object pool
        for(int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(bullet.transform) as Transform;
            bulletPool[i].gameObject.SetActive(false);
        }
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
}
