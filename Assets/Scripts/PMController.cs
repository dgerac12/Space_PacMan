using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PMController : MonoBehaviour
{
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject mainMenu;

    void Awake()
    {
        pauseMenu = this.gameObject;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    public void ExitToMM()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
