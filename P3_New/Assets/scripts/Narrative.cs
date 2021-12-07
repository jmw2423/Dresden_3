using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrative : MonoBehaviour
{

    public GameObject levelAdvanced;
    public bool gameIsPaused;
    public GameObject NarrativeOf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PauseForNarrative();
    }

    public void PauseForNarrative()
    {
        NarrativeOf.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
    public void ResumeGame()
    {
        NarrativeOf.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
