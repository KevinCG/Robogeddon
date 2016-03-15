using UnityEngine;
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

    //Holds health pickup object
    public GameObject healthPickup;

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
                child.GetComponent<Renderer>().material.color = flashColor;
            }
            else
            {
                foreach (Transform childInner in child)
                {
                    if (childInner.gameObject.name != ("HealthTrigger"))
                    {
                        childInner.GetComponent<Renderer>().material.color = flashColor;
                    }
                }
            }
        }

        yield return new WaitForSeconds(.3f);

        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                child.GetComponent<Renderer>().material.color = originalColor;
            }
            else
            {
                foreach (Transform childInner in child)
                {
                    if (childInner.gameObject.name != ("HealthTrigger"))
                    {
                        childInner.GetComponent<Renderer>().material.color = originalColor;
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
        int randomInt = Random.Range(1, 20);

        if(randomInt == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}