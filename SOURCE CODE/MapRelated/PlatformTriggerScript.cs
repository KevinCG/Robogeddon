using UnityEngine;
using System.Collections;

public class PlatformTriggerScript : MonoBehaviour
{
    //Reverse the direction of the platform
    //if a platform trigger was hit
   void OnTriggerEnter(Collider col)
    {
        if (col.name == "PlatformTrigger")
        {
            transform.parent.transform.GetComponent<MovingPlatformScript>().platformSpeed = -transform.parent.transform.GetComponent<MovingPlatformScript>().platformSpeed;
        }
    }
}
