using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private bool isPaused = false;

    public int totalStars = 0;

    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject pauseMenu;
    public GameObject inGameUI;
    public GameObject winMenuLvlOne;
    public GameObject winMenuLvlTwo;
    public GameObject gameOverMenu;
    public GameObject lvlOne;
    public GameObject lvlTwo;

    public GameObject player;
    
    private PlayerController playerController;
    private WinLvlController winLvlController;
    
    void Start()
    {
        mainMenu.SetActive(true);
        inGameUI.SetActive(false);
        pauseMenu.SetActive(false);
        winMenuLvlOne.SetActive(false);
        winMenuLvlTwo.SetActive(false);
        gameOverMenu.SetActive(false);
        controlsMenu.SetActive(false);

        lvlOne.SetActive(true);
        lvlTwo.SetActive(false);

        playerController = player.GetComponent<PlayerController>();
        winLvlController = winMenuLvlOne.GetComponent<WinLvlController>();
    }
    void Update()
    {
        //main menu
        if (mainMenu.activeSelf == true)
        {
            inGameUI.SetActive(false);
            Time.timeScale = 0;
            return;
        }

        if (controlsMenu.activeSelf == true)
        {
            inGameUI.SetActive(false);
            Time.timeScale = 0;
            return;
        }
        else if (mainMenu.activeSelf == false)
        {
            inGameUI.SetActive(true);
            Time.timeScale = 1;
        }

        //pause menu
        if(Input.GetKey(KeyCode.Escape) && mainMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            playerController.bgSource.Pause();
        }
        if(pauseMenu.activeSelf == false)
        {
            isPaused = false;
            playerController.bgSource.UnPause();
        }
        
        if(isPaused == true)
        {
            Time.timeScale = 0;
            return;
        }
        
        //conditionals
        if(playerController.lives == 0)
        {
            gameOverMenu.SetActive(true);
            //Time.timeScale = 0;
            return;
        }

        if(playerController.starCount == totalStars && winLvlController.lvlTwoStarted == false)
        {
            winMenuLvlOne.SetActive(true);
            playerController.lives = 2;
            playerController.starCount = 0;
            totalStars = 226;
            Time.timeScale = 0;
            return;
        }
        
        else if(playerController.starCount == totalStars && winLvlController.lvlTwoStarted == true)
        {
            winMenuLvlTwo.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        else
        {
            Time.timeScale = 1;
            return;
        }
    }
}
