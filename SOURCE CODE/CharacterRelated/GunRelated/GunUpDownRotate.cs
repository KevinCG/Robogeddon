using UnityEngine;
using System.Collections;

public class GunUpDownRotate : MonoBehaviour
{
    //speed used to rotate the guns
    private float rotateSpeed = 45f;

    //Gets player mouse input and rotates 
    //the guns up or down accordingly
    //clamps the rotations
    //NOTE --- CLAMPS NEED WORK
    void OnGUI()
    {
        //vector to hold translation values
        Vector3 rotateUpDown = Vector3.zero;

        //input from the mouse
        float playerRotationUpDown = Input.GetAxis("Mouse Y");

        //CLAMPS the rotations of the guns if they meet certain thresholds
        float currentXRot = transform.eulerAngles.x;
        if (currentXRot > 40 && currentXRot < 180)
        {
            transform.eulerAngles = new Vector3(40, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if (currentXRot < 320 && currentXRot > 180)
        {
            transform.eulerAngles = new Vector3(320f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        //regular rotation based off of player input
        else
        {
            //adding the input to the vector
            //used when no clamping needed
            rotateUpDown.y += playerRotationUpDown * rotateSpeed;
            rotateUpDown.z = 0;
            rotateUpDown *= Time.deltaTime;

            //handles the gun's differing rotations.
            if (gameObject.name == "MachineGunLeft")
            {
                transform.Rotate(-rotateUpDown);
            }
            else
            {
                transform.Rotate(rotateUpDown);
            }
        }
    }
}
