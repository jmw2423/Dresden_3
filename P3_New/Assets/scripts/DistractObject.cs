using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractObject : MonoBehaviour
{
    //Distract the enemy
    //Have them stop walking and change their fov towards the distraction.
    public void DistractE()
    {
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
