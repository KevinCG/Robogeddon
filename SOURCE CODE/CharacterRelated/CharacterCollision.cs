using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour
{
    /*This script handles the different types of
    **bot collisions (damage) with our character through the use 
    **of a trigger. All weighted differently due to
    **differences in speed/health etc
    **
    **Also handles the player jumping on moving platform
    */
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "BallBotAnimated(Clone)")
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.2f);
        }
        else if (col.gameObject.name == "TallBotAnimated(Clone)")
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.4f);
        }
        else if (col.gameObject.name == "FatBotAnimated(Clone)")
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.7f);
        }   
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "BallBotAnimated(Clone)")
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.2f);
        }
        else if (col.gameObject.name == "TallBotAnimated(Clone)" || col.name == "unitychan(Clone)") 
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.4f);
        }
        else if (col.gameObject.name == "FatBotAnimated(Clone)")
        {
            transform.parent.transform.GetComponent<Character>().setPlayerHealth(-.7f);
        }
        if (col.name == "MovingPlatform")
        {
            transform.parent.transform.SetParent(col.transform.parent.transform);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "MovingPlatform")
        {
            transform.parent.transform.SetParent(null);
        }
    }
}
