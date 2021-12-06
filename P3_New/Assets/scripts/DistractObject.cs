using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractObject : MonoBehaviour
{
    public Sprite OrigSprite;
    public Sprite[] BurningSprites;

    public int burnLoops;
    private int currSprite;
    private float burnTimer;

    private bool burnt;
    //Distract the enemy
    //Have them stop walking and change their fov towards the distraction.
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = OrigSprite;
        burnt = false;
        currSprite = 0;
    }

    public void Update()
    {
        if (burnt)
        {
            if (burnLoops > 0)
            {
                if (burnTimer > 0)
                {
                    burnTimer -= Time.deltaTime;
                }
                else
                {
                    currSprite++;
                    if (currSprite >= BurningSprites.Length) currSprite = 0;
                    GetComponent<SpriteRenderer>().sprite = BurningSprites[currSprite];

                    burnLoops--;
                    burnTimer = 1f;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = OrigSprite;
                GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, 1);
            }
        }
    }

    public void DistractE()
    {
        if (!burnt)
        {
            burnt = true;
            burnTimer = 1f;
            GetComponent<SpriteRenderer>().sprite = BurningSprites[0];
            Debug.Log("DISTACT!");


            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 1.5f);
            //for each in array
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy_1")
                {
                    Debug.Log("DISTACT!");
                    //Make sure that enemy and fov can go back to regular business
                    enemy guy = (enemy)hitCollider.GetComponent(typeof(enemy));
                    guy.CauseDistracted(this.transform.position);
                    //Enemy Orient
                    //Fov Orient
                }
            }
        }
    }
}
