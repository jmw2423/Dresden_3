using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool visible; // whether or not the player is visible to enemies
    private float castTimer; // tracks how much time until the player finishes casting a spell
    private int castType; // tracks which spell is being cast. 0 - Knockout, 1 - Distract, 2 - Invisibility

    public int invisCharges; // count of invisibility charges the player currently has
    [SerializeField] private int maxCharges = 3; // max number of invis charges the player may hold
    private float invisTimer; // tracks invisibility time
    [SerializeField] private float invisLength = 3; // length of invisibility spell
    private float chargeTimer; // tracks invis charge recharge time
    [SerializeField] private float rechargeTime = 5; // length of time to recharge an invis charge

    private BoxCollider2D boxCollider;
    private Vector3 move;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    private void Start()
    {
        visible = true;
        castTimer = 0;
        castType = -1;
        invisCharges = maxCharges;
        chargeTimer = 0;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // If the player isn't casting, accept input as usual
        if (castTimer == 0)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            move = new Vector3(x, y, 0);

            //y axis
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, move.y), Mathf.Abs(move.y * Time.deltaTime), LayerMask.GetMask("blocking", "actor"));
            if (hit.collider == null)
            {
                transform.Translate(0, move.y * Time.deltaTime, 0);

            }
            //x axis
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(move.x, 0), Mathf.Abs(move.x * Time.deltaTime), LayerMask.GetMask("blocking", "actor"));
            if (hit.collider == null)
            {
                transform.Translate(move.x * Time.deltaTime, 0, 0);
            }

            if (visible) // The player may only use spells or recharge invis charges while visible
            {
                // check for spell cast. Else-if to prevent multiple spell casts at once
                if (Input.GetMouseButtonDown(0))
                {
                    castTimer = .1f;
                    castType = 0;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    castTimer = .5f;
                    castType = 1;
                }
                else if (Input.GetKeyDown("space") && invisCharges > 1)
                {
                    invisCharges--;
                    castTimer = 2;
                    castType = 2;
                }

                // recharge invis charges
                if (invisCharges < maxCharges)
                {
                    chargeTimer += Time.deltaTime;
                    if (chargeTimer >= rechargeTime)
                    {
                        invisCharges++;
                        chargeTimer = 0;
                    }
                }
            }
            else // otherwise handle invisibility timer
            {
                invisTimer -= Time.deltaTime;
                if (invisTimer <= 0)
                {
                    visible = true;
                    invisTimer = 0;
                    this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                }
            }
        }
        else // If the player is casting, they are held in place for the length of the cast (dependent on spell) until the spell is finished casting
        {
            castTimer -= Time.deltaTime;
            if(castTimer <= 0)
            {
                switch (castType) // finish casting the spell
                {
                    case 0: // Knockout spell
                        break;
                    case 1: // Distract spell
                        break;
                    case 2: // Invisibility spell
                        visible = false;
                        invisTimer = invisLength;
                        this.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1, 0.5f);
                        break;
                }
                castTimer = 0;
                castType = -1;
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}

