using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
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
    private int orientation;

    public int currClues; // the amount of clues held for the current level. RESET BETWEEN LEVELS
    public int levelClues; // the number of clues in the current level. SET WHEN STARTING GAME OR USING A DOOR TO PROCEED TO THE NEXT LEVEL

    public Text currClueText;
    public Text levelClueText;
    public Animator animator;
    Vector2 movement;
    bool walking;


    // Start is called before the first frame update
    private void Start()
    {
        currClues = 0;
        pressed = false;
        visible = true;
        walking = false;
        castTimer = 0;
        castType = -1;
        invisCharges = maxCharges;
        chargeTimer = 0;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) == false)
        {
            pressed = false;
        }
        // If the player isn't casting, accept input as usual
        if (pressed == false)
        {


            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
            if(movement != Vector2.zero)
            {
                if(!walking)
                {
                    StartCoroutine(walkSound());
                }
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }
            
            animator.SetFloat("Speed", movement.sqrMagnitude);

            move = new Vector3(movement.x * 1.25f, movement.y * 1.25f, 0);
            
            if (movement.x < 0)
            {
                orientation = 3;
            }
            else if (movement.x > 0)
            {
                orientation = 1;
            }
            else if (movement.y < 0)
            {
                orientation = 0;
            }
            else if (movement.y > 0)
            {
                orientation = 2;
            }
            Debug.Log(orientation);
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

        if (castTimer == 0 && invisPotion.value != 100)
        {
            if (visible) // The player may only use spells or recharge invis charges while visible
            {
                // check for spell cast. Else-if to prevent multiple spell casts at once
                if (Input.GetMouseButtonDown(0))
                {
                    castTimer = .1f;
                    castType = 0;
                }
                else if (Input.GetMouseButtonDown(1) && this.GetComponent<Distraction>().useable)
                {
                    castTimer = .1f;
                    castType = 1;
                }
                else if (Input.GetKey("space") && invisCharges > 0)
                {
                    if(invisPotion.value <= 0)
                    {
                        soundManagerScript.PlaySound("Drinking");
                    }
                    pressed = true;
                    invisPotion.value += 70 * Time.deltaTime;
                }

                if (invisPotion.value > 0)
                {
                    invisPotion.value -= 20 * Time.deltaTime;
                }

                if (invisPotion.value >= 100)
                {
                    invisCharges--;
                    castTimer = .01f; // invisPotion is the timer on this spell
                    castType = 2;
                    invisPotion.value = 99;
                }
                else if (invisPotion.value < 0)
                {
                    invisPotion.value = 0;
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
            castTimer -= Time.deltaTime;
            
            if (castTimer <= 0)
            {
                switch (castType) // finish casting the spell
                {
                    case 0: // Knockout spell
                        this.GetComponent<PKnockout>().KO(orientation);
                        soundManagerScript.PlaySound("PlayerHit");
                        break;
                    case 1: // Distract spell
                        this.GetComponent<Distraction>().shoot(Input.mousePosition);
                        soundManagerScript.PlaySound("Fireball");
                        break;
                    case 2: // Invisibility spell
                        visible = false;
                        soundManagerScript.PlaySound("Invis");
                        tag = "PlayerInvis";
                        invisTimer = invisLength;
                        this.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 1, 0.5f);
                        break;
                }
                castTimer = 0;
                castType = -1;
            }
        }
        if (visible == false)
        {
            invisPotion.value -= 33 * Time.deltaTime;
        }

        currClueText.text = currClues.ToString();
        levelClueText.text = levelClues.ToString();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public int GetOrientation()
    {
        return orientation;
    }
    IEnumerator walkSound()
    {
        walking = true;
        soundManagerScript.PlaySound("Walk");
        yield return new WaitForSeconds(10);
        walking = false;
    }

    public void playDoorSound()
    {
        soundManagerScript.PlaySound("Door");
    }
}

