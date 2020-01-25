using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLvlController : MonoBehaviour
{
    public GameObject lvlOne;
    public GameObject lvlTwo;
    public GameObject player;

    public GameObject uiManager;
    public bool lvlTwoStarted = false;

    private PlayerController playerController;

    private UIController uiController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        uiController = uiManager.GetComponent<UIController>();
    }
    void Update()
    {
        if(lvlTwoStarted == true)
        {
            playerController.lives = 2;
            playerController.starCount = 0;
            uiController.totalStars = 226;
            lvlOne.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    public void GoToLvlTwo()
    {
        lvlTwo.SetActive(true);
        lvlTwoStarted = true;
        playerController.transform.position = playerController.playerStart;
    }
}
