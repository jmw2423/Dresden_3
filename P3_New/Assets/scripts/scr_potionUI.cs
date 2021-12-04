using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_potionUI : MonoBehaviour
{
    public Sprite fullPotion;
    public Sprite emptyPotion;
    public List<GameObject> potionSlots;
    private player Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = this.transform.GetComponentInParent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < potionSlots.Count; i++)
        {
            if(Player.invisCharges > i)
            {
                potionSlots[i].GetComponent<SpriteRenderer>().sprite = fullPotion;
            }
            else
            {
                potionSlots[i].GetComponent<SpriteRenderer>().sprite = emptyPotion;
            }
        }
    }
}
