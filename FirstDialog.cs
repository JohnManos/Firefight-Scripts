using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FirstDialog : MonoBehaviour
{
    //public Color textColor = Color.white;

    private Text message;
    private bool notHit;
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("First Message").GetComponent<Text>();
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
        float ogPlayerSpeed = other.gameObject.GetComponent<Player_Move_Update>().playerSpeed;
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = 0;
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(false);
        message.text = "Behold the green object ahead. ";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "That is the essence of life.";
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "The essence of that which came before. ";
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "Does it not remind you? Do you not recall?";
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "Take it. Maybe enough of it can bring you back to life before the ruin.";
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        //

        //let player move again
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(true);
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
        notHit=false;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
