using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (other.tag == "flameable")
        {
            Debug.Log("TESTING");
            DistractObject distract = (DistractObject)other.GetComponent(typeof(DistractObject));
            distract.DistractE();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}

