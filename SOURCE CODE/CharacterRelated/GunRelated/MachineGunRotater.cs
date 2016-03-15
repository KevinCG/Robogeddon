using UnityEngine;
using System.Collections;

public class MachineGunRotater : MonoBehaviour
{
	//Rotating the tubes on the turret.
    //Only do this when the player is holding down the shoot button.
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0, 0, 200f * Time.deltaTime, Space.Self);
        }
    }
}
