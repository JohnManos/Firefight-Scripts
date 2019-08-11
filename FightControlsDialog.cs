using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FightControlsDialog : MonoBehaviour
{
    //public Color textColor = Color.white;
    
    private Text message;
    private bool notHit;
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Fight Tutorial Message").GetComponent<Text>();
        //message.color = textColor; //Color.white;
        message.text = "";
        notHit=true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Player" && notHit)
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
        //int ogPlayerSpeed = other.gameObject.GetComponent<Player_Move_Update>().playerSpeed;
        message.text = "Press F to attack with this weapon.";
        //other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = 0;
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        //while (!Input.anyKeyDown) {
        //yield return null;
        //}
        //

        //let player move again
        //other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
        notHit=false;
        return null;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
