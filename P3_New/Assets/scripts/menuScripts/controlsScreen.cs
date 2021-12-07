using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class controlsScreen : MonoBehaviour
{
    public Button menuButton;

    // Start is called before the first frame update
    void Start()
    {

        menuButton.onClick.AddListener(goToMenuButton);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void goToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
