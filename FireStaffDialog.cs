using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FireStaffDialog : MonoBehaviour
{
    //public Color textColor = Color.white;
    
    private Text message;
    private bool notHit;
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Tutorial Message").GetComponent<Text>();
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

        message.text = "Press Q to switch weapons.";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        
        message.text = "The fire staff attacks much more quickly than the sword.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "You can also use it while moving.";

        //let player move again
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(true);

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
