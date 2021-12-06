using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winGame : MonoBehaviour
{
    public Button MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton.onClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
