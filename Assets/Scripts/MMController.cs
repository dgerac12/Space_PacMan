using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMController : MonoBehaviour
{
    public GameObject player;
    private GameObject mainMenu;

    private PlayerController playerController;
    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        mainMenu = this.gameObject;
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        playerController.timeLeft = 3;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
