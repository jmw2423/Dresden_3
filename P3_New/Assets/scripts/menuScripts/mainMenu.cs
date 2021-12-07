using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public Button controlsButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartTheGame);
        exitButton.onClick.AddListener(ExitTheGame);
        controlsButton.onClick.AddListener(controls);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
