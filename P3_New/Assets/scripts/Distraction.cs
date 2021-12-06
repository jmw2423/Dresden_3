using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireBall;
    //private Vector3 mousePos;

    //Cool down can be changed in engine
    public float cooldown;
    public bool useable;
    public Animator animator;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Input.mousePosition;
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (useable)
        //    {
        //        shoot();
        //        
        //    }
        //}
    }
    //Gets the direction that the bullet is supposed to travel based on mouse position and player position.
    public void shoot(Vector3 mousePos)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        Quaternion fbRotation = Quaternion.Euler(0f, 0f, angle);
        Debug.Log(angle);
        Instantiate(fireBall, transform.position, fbRotation);
        StartCoroutine(fbCooldown());
    }
    IEnumerator fbCooldown()
    {
        Debug.Log("HELLO?!");
        animator.SetBool("Casting", true);
        useable = false;
        yield return new WaitForSeconds(cooldown);
        useable = true;
        animator.SetBool("Casting", false);
    }

}
