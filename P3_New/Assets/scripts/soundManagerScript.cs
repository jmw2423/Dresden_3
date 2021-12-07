using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip playerWalk, PlayerFireBall, PlayerInvis, PlayerHit;
    static AudioSource audioSource;
    void Start()
    {
      //  playerWalk = Resources.Load<AudioClip>("PlayerWalk");
        PlayerFireBall = Resources.Load<AudioClip>("Fireball");
        PlayerInvis = Resources.Load<AudioClip>("Invis");
        PlayerHit = Resources.Load<AudioClip>("PlayerHit");
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
        }
    }
}
