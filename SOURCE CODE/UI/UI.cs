using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //used to update UI
    public Text playerKillsText;
    public Text timeElapsedText;
    public Text waveNumberText;
    public Text healthText;
    public Text gameOverText;
    public Slider healthSlider;
    public Button restartLevelButton;
    public GameObject player;
	
    void Start()
    {
        //Make these non active until the game is over
        restartLevelButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }
	void Update ()
    {
        updateUIText();
        updateHealthSlider();

        //"Pauses game" when player has no more health
        if (Time.timeScale == 0)
        {
            gameOverMenu();
        }
    }

    //updates the UI text each update()
    void updateUIText()
    {
        timeElapsedText.text = "Time Elapsed: " + Time.timeSinceLevelLoad.ToString("F2");
        playerKillsText.text = "Kills: " + Enemy.deadBots;
        waveNumberText.text = "Wave: " + EnemySpawner.waveNumber.ToString();
        healthText.text = player.GetComponent<Character>().getPlayerHealth().ToString("F0") + " / 100";
    }

    //updates the players health on the UI
    void updateHealthSlider()
    {
        healthSlider.value = player.GetComponent<Character>().getPlayerHealth();
    }

    //Says game over and displays a restart button 
    void gameOverMenu()
    {
        restartLevelButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);     
    }

    //used to restart the scene so the player
    //can play it again
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
}
