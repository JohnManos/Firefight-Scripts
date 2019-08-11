//using System.Diagnostics;
//using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReplayDialog : MonoBehaviour
{
    //public Color textColor = Color.white;

    private Text message;
    private bool notHit = true;
    
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Replay text").GetComponent<Text>();
        //message.color = textColor; //Color.white;
        message.text = "";
        notHit=true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Weapon_Status>().GetWeapon() == "fireStaff")
        {
            StartCoroutine(displayText(other));

        }
        //Message to display if main dialogue is done
        /*else if(other.gameObject.tag == "Player" && !notHit)
        {
            message.text = "You must now move forward";
        }*/
    }
    IEnumerator displayText(Collider2D other)
    {
        message.text = "Press R to replay level one with the fire staff.";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.GetButtonDown("Replay"));

        message.text = "";
        notHit=false;
        SceneManager.LoadScene(4);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }

    public bool GetNotHit()
    {
        return notHit;
    }

    public void SetNotHit(bool newHit)
    {
        notHit = newHit;
    }
}
