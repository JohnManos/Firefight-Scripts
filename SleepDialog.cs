using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SleepDialog : MonoBehaviour
{
    //public Color textColor = Color.white;

    private Text message;
    private bool notHit = true;
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Sword Message").GetComponent<Text>();
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
        message.text = "You feel a strange exhaustion befall you. ";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "You can't help but follow the irrestistable pull of a bizarre slumber....";
        do {
        yield return null;
        } while (!Input.anyKeyDown);

        //let player move again
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(true);
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
        notHit=false;

        SceneManager.LoadScene(3);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }

    public bool getNotHit()
    {
        return notHit;
    }
}
