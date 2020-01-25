using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLvlController : MonoBehaviour
{
    public GameObject lvlOne;
    public GameObject lvlTwo;
    public GameObject player;

    public bool lvlTwoStarted = false;

    private PlayerController playerController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        if(lvlTwoStarted == true)
        {
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
