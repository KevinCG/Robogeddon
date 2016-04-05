using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    //character's char controller
    CharacterController charController;

    //Player variables that have to do
    //with player movement/rotation
    public float playerMovSpeed = 25f;
    private float playerJumpSpeed = 1f;
    [Range(10,100)]
    public  float playerRotateSpeed = 70f;
    private float playerGravity = -.05f;
    private float playerYVel = 0f;
    private bool playerJumping = false;

    //players health
    private float playerHealth;
    public const int MAXPLAYERHEALTH = 100;

	void Start ()
    {
        //getting the character controller
        charController = gameObject.GetComponent<CharacterController>();

        //setting the player's health
        playerHealth = 100f;

        //Get rid of the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //Setting the game back to regular speed
        Time.timeScale = 1;
    }
	
	void Update ()
    {
        //if the player should be dead
        //"pause the game"
        if(playerHealth <= 0)
        {
            playerHealth = 0;
            //Get rid of the cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }

        //getting user input for movement.
        float HorinzontalMov = Input.GetAxis("Horizontal");
        float verticalMov = Input.GetAxis("Vertical");

        //These will be used move/rotate player.
        //Initialized to (0, 0, 0).
        Vector3 amountForBack = Vector3.zero;
        Vector3 amountLeftRight = Vector3.zero;

        //Here we apply the changes to our amounts.
        amountForBack += transform.forward * verticalMov * playerMovSpeed;
        amountLeftRight += transform.right * HorinzontalMov * playerMovSpeed;

        //Multiple by time.deltaTime
        amountForBack *= Time.deltaTime;
        amountLeftRight *= Time.deltaTime;

        //NOTE JUMPING CAN BE IMPROVED BY MAKING ANY TAP OF THE SPACE
        //BAR THE SAME AMOUNT OF JUMP --- NOT IMPLEMENTED YET

        //set the player to not jumping.
        if (playerJumping && charController.isGrounded)
        {
            playerJumping = false;
        }

        //Player Jump if player is not jumping/is on ground.
        if (Input.GetKeyDown("space") && !playerJumping)
        {
            playerYVel = playerJumpSpeed;
            playerJumping = true;
        }

        //Set the player speed to 0
        //which allows gravity to kick in.
        if (Input.GetKeyUp("space") && playerYVel > 0)
        {
            playerYVel = 0;
        }

        //add gravity if the character is in the air.
        if (!charController.isGrounded)
        {
            playerYVel += playerGravity;
        }

        //limits the speed the player falls.
        playerYVel = Mathf.Clamp(playerYVel, -3, playerJumpSpeed);

        //Actually apply gravity to our player.
        amountLeftRight.y += playerYVel;

        //Actually apply the movements to our character.
        charController.Move(amountForBack);
        charController.Move(amountLeftRight);      
    }

    //Handles mouse input to rotate the player.
    //Done in OnGUI because in update it causes
    //choppy behavior
    void OnGUI()
    {
        Vector3 amountToRotate = Vector3.zero;
        float playerRotation = Input.GetAxis("Mouse X");
        amountToRotate.y += playerRotation * playerRotateSpeed;
        amountToRotate *= Time.deltaTime;
        transform.Rotate(amountToRotate);   
    }

    //Get the player's health
    public float getPlayerHealth()
    {
        return playerHealth;
    }

    //Set the player's health
    //this takes an amount to decrease the hp by
    //used for enemies to dmg player
    public void setPlayerHealth(float amountToChange)
    {
        playerHealth = playerHealth + amountToChange;
    }
    
    //Set player's health to 100
    //used by health pickup
    public void setPlayerHealthToMAX()
    {
        playerHealth = MAXPLAYERHEALTH;
    }
}
