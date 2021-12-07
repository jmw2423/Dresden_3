using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip playerWalk, PlayerFireBall, PlayerInvis, PlayerHit, Alert, Door, Drinking, Paper;
    static AudioSource audioSource;
    void Start()
    {
        playerWalk = Resources.Load<AudioClip>("walk");
        PlayerFireBall = Resources.Load<AudioClip>("Fireball");
        PlayerInvis = Resources.Load<AudioClip>("Invis");
        PlayerHit = Resources.Load<AudioClip>("PlayerHit");
        Alert = Resources.Load<AudioClip>("Alert");
        Door = Resources.Load<AudioClip>("door");
        Drinking = Resources.Load<AudioClip>("Drinking");
        Paper = Resources.Load<AudioClip>("Paper");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
           // case "PlayerWalk":
            //    audioSource.PlayOneShot(playerWalk);
           //     break;
            case "Fireball":
                audioSource.PlayOneShot(PlayerFireBall);
                break;
            case "Invis":
                audioSource.PlayOneShot(PlayerInvis);
                break;
            case "PlayerHit":
                audioSource.PlayOneShot(PlayerHit);
                break;
            case "Walk":
                audioSource.PlayOneShot(playerWalk);
                break;
            case "Alert":
                audioSource.PlayOneShot(Alert);
                break;
            case "Paper":
                audioSource.PlayOneShot(Paper);
                break;
            case "Door":
                audioSource.PlayOneShot(Door);
                break;
            case "Drinking":
                audioSource.PlayOneShot(Drinking);
                break;
        }
    }
}
