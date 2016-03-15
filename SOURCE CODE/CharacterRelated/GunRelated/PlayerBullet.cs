using UnityEngine;
using System.Collections;


public class PlayerBullet : MonoBehaviour
{
    //speed of bullet
    private float bulletSpeed = 310f;

    //amount of damage the bullet inflicts
    private static int bulletDamage = 5;
    
    //keeps track of bullets original position
    private Vector3 startPos;

	void Start()
    {
        //getting the bullet start position.
        startPos = transform.position;

    }
	void Update ()
    {
        //Moving the bullet
        transform.Translate(transform.up * bulletSpeed * Time.deltaTime, Space.World);

        //Check if the bullet has traveled a certain distance
        //if it has, destroy it because there is no longer a 
        //need for it.
        Vector3 currentPos = transform.position;
        if (currentPos.x > Mathf.Abs(startPos.x + 200) || currentPos.y > Mathf.Abs(startPos.y + 200) || 
            currentPos.z > Mathf.Abs(startPos.z + 200))
        {
            gameObject.SetActive(false);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        //if 2 bullets hit each other, destroy both
        if(col.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
        }

        //if not, just destroy the bullet
        else
        {
            gameObject.SetActive(false);
        }
    }

    //gets the bullet damage
    public static int getBulletDamage()
    {
        return bulletDamage;
    }

    //sets the bullet damage
    //not used but could be used for double damage 
    //and other powerups
    public static void setBulletDamage(int newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }
}
