using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : Collectable
{
    public static List<GameObject> items = new List<GameObject>();

    protected override void OnCollect(GameObject player)
    {
        if (!collected)
        {
            items.Add(this.gameObject);
            Destroy(this.gameObject);
            player.GetComponent<player>().currClues++;
        }

    }


}

