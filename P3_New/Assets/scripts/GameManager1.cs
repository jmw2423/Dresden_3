using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    /*public GameObject advance1_2;
    public GameObject advance2_3;
    public GameObject advance3_4;
    public GameObject advance4_5;*/
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public GameObject door5;

    public int roomNum; 
    public Slider invis;
    public Slider alert;
    //private GameObject back;
    public GameObject back;
    public GameObject fill;

    private bool filling;

    public GameObject introNarr;

    // Start is called before the first frame update
    void Start()
    {
        roomNum = 1;
        filling = true;

        invis.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 0);
        invis.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        alert.value = 0;
        //back = alert.transform.GetChild(0).gameObject;
        //back.GetComponent<Image>.enabled = false;
        //col.colors
        alert.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 0);
        alert.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            introNarr.SetActive(false);
        }

        if(alert.value == 100)
        {
            SceneManager.LoadScene("GameOver");
        }
        if(alert.value > 0)
        {
            alert.value -= 10 * Time.deltaTime;
            alert.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 1);
            //alert.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        else
        {
            alert.value = 0;
            alert.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 0);
            //alert.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        if(invis.value > 0)
        {
            if(filling)
            {
                invis.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(.2f, 0, .8f, 1);
                invis.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            if(invis.value == 99)
            {
                filling = false;
                invis.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0, 1, 1, 1);
                invis.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            
        }
        else
        {
            filling = true;
            invis.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 0);
            invis.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        if(Clue.items.Count >= 4)
        {
            roomNum++;
            Clue.items.Clear();
        }
        if(roomNum == 2)
        {
            Destroy(door1);
        }
        if(roomNum == 3)
        {
            Destroy(door2);
        }
        if (roomNum == 4)
        {
            Destroy(door3);
        }
        if (roomNum == 5)
        {
            Destroy(door4);
        }

       

    }
}
