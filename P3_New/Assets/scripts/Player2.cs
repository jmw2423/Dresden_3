using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public bool pressed;

    public Slider invisPotion;

    public bool visible; // whether or not the player is visible to enemies
    public float castTimer; // tracks how much time until the player finishes casting a spell
    public int castType; // tracks which spell is being cast. 0 - Knockout, 1 - Distract, 2 - Invisibility

    public int invisCharges; // count of invisibility charges the player currently has
    [SerializeField] private int maxCharges = 3; // max number of invis charges the player may hold
    public float invisTimer; // tracks invisibility time
    [SerializeField] private float invisLength = 3; // length of invisibility spell
    private float chargeTimer; // tracks invis charge recharge time
    [SerializeField] private float rechargeTime = 5; // length of time to recharge an invis charge

    private BoxCollider2D boxCollider;
    private Vector3 move;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    private void Start()
    {
        pressed = false;
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
        if(Input.GetKey(KeyCode.Space) == false)
        {
            pressed = false;
        }
        // If the player isn't casting, accept input as usual
        if (pressed == false)
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
        }

        if(castTimer == 0 && invisPotion.value != 100)
        {             if (visible) // The player may only use spells or recharge invis charges while visible
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
                else if (Input.GetKey("space") && invisCharges > 1)
                {
                    pressed = true;
                    invisPotion.value += 50 * Time.deltaTime;

                }

                if(invisPotion.value == 100)
                {
                    invisCharges--;
                    castTimer = 2;
                    castType = 2;
                    invisPotion.value = 99;
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
                //invisPotion.value -= 33 * Time.deltaTime;
                if (invisTimer <= 0)
                //if(invisPotion.value == 0)
                {

                    visible = true;
                    tag = "Player";
                    invisTimer = 0;
                    this.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
                }
            }
        }

         // If the player is casting, they are held in place for the length of the cast (dependent on spell) until the spell is finished casting
        else
        {
           
            if(castTimer == 2)
           
            switch (castType) // finish casting the spell
            {
                case 0: // Knockout spell
                    break;
                case 1: // Distract spell
                    break;
                case 2: // Invisibility spell
                    visible = false;
                    tag = "PlayerInvis";
                    invisTimer = invisLength;
                    this.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1, 0.5f);
                    break;
            }
            castTimer = 0;
            castType = -1;
          
        } 
        if (visible == false)
        {
            invisPotion.value -= 33 * Time.deltaTime;
        }
       
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
