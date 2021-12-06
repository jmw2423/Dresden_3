using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    //public GameObject exitGame;
    // public Button exitGameButton;

    //public GameObject continueGame;
    //public Button continueGameButton;
    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        ResumeGame();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();

            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (gameIsPaused)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameIsPaused)
            {
                Application.Quit();
            }
        }
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameIsPaused)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }
        }*/
    }

    public void PauseGame()
    {
        //continueGame.SetActive(true);
        // exitGame.SetActive(true);
        //continueGameButton.interactable = true;
        //exitGameButton.interactable = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {        //continueGameButton.interactable = false;
        //exitGameButton.interactable = false;
        //continueGame.SetActive(false);
        //exitGame.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
