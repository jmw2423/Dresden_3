using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : Collectable
{
    public static List<GameObject> items = new List<GameObject>();

    protected override void OnCollect()
    {
        if (!collected)
        {
            //Debug.Log("Collected");
            if (Input.GetKeyDown(KeyCode.E))
            {
                items.Add(this.gameObject);
                Destroy(this.gameObject);

            }

        }

    }


}

