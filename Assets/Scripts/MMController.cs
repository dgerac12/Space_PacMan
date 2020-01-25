using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMController : MonoBehaviour
{
    private GameObject mainMenu;

    void Awake()
    {
        mainMenu = this.gameObject;
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
