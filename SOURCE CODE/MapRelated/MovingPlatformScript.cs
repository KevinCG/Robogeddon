using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour
{
    //speed of platform
    public int platformSpeed = 10;

	void Update ()
    {
        //move the platform at a constant rate
        transform.Translate(platformSpeed * Time.deltaTime, 0, 0);
	}
}
