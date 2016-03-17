using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
    //Holds player gameobject
    private GameObject player;

    void Start()
    {
        //get the player object
        player = GameObject.Find("Player");
    }

	void OnTriggerEnter(Collider col)
    {
        //if the player ran into the health pickup
        if (col.name == "Player")
        {
            //only activate health if the player needs health
            //Give player 100 health and destroy health pickup
            if (player.GetComponent<Character>().getPlayerHealth() < Character.MAXPLAYERHEALTH)
            {
                player.GetComponent<Character>().setPlayerHealthToMAX();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
