using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class doorScript : MonoBehaviour
{

    public bool win;
    public int nextLvlClues;
    public float nextLvlX;
    public float nextLvlY;
    public Text nextLvlCue;
    public doorScript nextDoor;
    private player playerScr;

    public bool gameIsPaused;
    public GameObject NarrativeOf;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        playerScr = FindObjectOfType<player>();
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (gameIsPaused)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    ResumeGame();
                }
            }
            else
            {
                if (playerScr.currClues == playerScr.levelClues)
                {
                    this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    nextLvlCue.gameObject.SetActive(true);

                    if (Vector3.Distance(playerScr.transform.position, transform.position) < .5f)
                    {
                        playerScr.playDoorSound();
                        if (win)
                        {
                            SceneManager.LoadScene("Win");
                        }
                        else
                        {
                            // narrative stuff
                            nextLvlCue.gameObject.SetActive(false);
                            PauseForNarrative();
                        }
                    }
                }
            }
        }
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

        playerScr.currClues = 0;
        playerScr.levelClues = nextLvlClues;
        playerScr.transform.position = new Vector3(nextLvlX, nextLvlY, 0);
        nextDoor.active = true;
        active = false;
    }
}

