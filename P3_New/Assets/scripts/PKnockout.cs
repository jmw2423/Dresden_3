//Reese Lodwick
//Knockout script
//Gets what directon the enemy is facing and checks if player character is also facing that way

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKnockout : MonoBehaviour
{
    // Start is called before the first frame update
    private float Cooldown;
    public float strtCooldown;

    public Transform atackPos;
    public LayerMask whatIsEnemies;
    public float range;
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        // if (Cooldown >= 0)
        // {
        //     if (Input.GetKeyDown(KeyCode.Q))
        //     {
        //         KO(this.GetComponent<player>().GetOrientation());
        //         
        //     }
        //     Cooldown = strtCooldown;
        // }
        // if (Cooldown >= 0)
        // {
        //     Cooldown -= Time.deltaTime;
        // }
    }
    //KO Checks the users orientation passed through a parameter
    //If that orientation is 
    public void KO(int orientation)
    {
        if (orientation == 0)
        {
            Vector2 newAttackPos = new Vector2(atackPos.position.x, atackPos.position.y - .42f);
            Vector2 vBox = new Vector2(this.transform.position.x, this.transform.position.y - .22f);
            Collider2D[] Enemy = Physics2D.OverlapCircleAll(newAttackPos, .2f);
            for (int x = 0; x < Enemy.Length; x++)
            {
                if (Enemy[x].tag == "Enemy_1")
                {
                    Debug.Log("OR1");
                    if (Enemy[x].GetComponent<enemy>().GetOrientation() == 0)
                    {
                        Enemy[x].GetComponent<enemy>().Kod();
                        soundManagerScript.PlaySound("PlayerHit");
                    }
                }
            }
        }
        else if (orientation == 1)
        {

            Vector2 hBox = new Vector2(5f, 5f);
            Vector2 newAttackPos = new Vector2(this.transform.position.x + .32f, this.transform.position.y);
            Collider2D[] Enemy = Physics2D.OverlapCircleAll(newAttackPos, .2f);
            for (int x = 0; x < Enemy.Length; x++)
            {
                if (Enemy[x].tag == "Enemy_1")
                {
                    if (Enemy[x].GetComponent<enemy>().GetOrientation() == 1)
                    {
                        Debug.Log("OR2");
                        Enemy[x].GetComponent<enemy>().Kod();
                    }
                }
            }
        }
        else if (orientation == 2)
        {

            Vector2 newAttackPos = new Vector2(this.transform.position.x, this.transform.position.y + .42f);
            Vector2 vBox = new Vector2(5f, 5f);
            Collider2D[] Enemy = Physics2D.OverlapCircleAll(newAttackPos, .2f);
            for (int x = 0; x < Enemy.Length; x++)
            {
                if (Enemy[x].tag == "Enemy_1")
                {
                    if (Enemy[x].GetComponent<enemy>().GetOrientation() == 2)
                    {
                        Debug.Log("OR3");
                        Enemy[x].GetComponent<enemy>().Kod();
                    }
                }
            }
        }
        else if (orientation == 3)
        {
            Vector2 newAttackPos = new Vector2(this.transform.position.x - .32f, this.transform.position.y);
            Vector2 hBox = new Vector2(5f, 5f);
            Collider2D[] Enemy = Physics2D.OverlapCircleAll(newAttackPos, .2f);
            for (int x = 0; x < Enemy.Length; x++)
            {
                Debug.Log(Enemy[x].tag);
                if (Enemy[x].tag == "Enemy_1")
                {
                    if (Enemy[x].GetComponent<enemy>().GetOrientation() == 3)
                    {
                        Debug.Log("OR4");
                        Enemy[x].GetComponent<enemy>().Kod();
                    }
                }
            }
        }

        StartCoroutine(Anim());
    }
    IEnumerator Anim()
    {
        animator.SetBool("Casting", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Casting", false);
    }
}
