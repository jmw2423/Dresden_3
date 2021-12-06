using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player" || coll.tag == "PlayerInvis") // && coll.gameObject.GetComponent<player>().visible)
        {
            OnCollect(coll.gameObject);
        }
    }

    protected virtual void OnCollect(GameObject player)
    {
        collected = true;
    }
}

