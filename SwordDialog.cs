using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwordDialog : MonoBehaviour {
    //public Color textColor = Color.white;

    private Text message;
    private bool hit = false;

    void Start() {
        // Ensure there is Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Sword Message").GetComponent<Text>();
        //message.color = textColor; //Color.white;
        message.text = "";
        hit=false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Begin dialog when trigger is entered for the first time        
        if (other.gameObject.tag == "Player" && !hit) {
            StartCoroutine(displayText(other));
        }
        //Message to display if main dialogue is done
        /*else if(other.gameObject.tag == "Player" && hit)
        {
            message.text = "You must now move forward";
        }*/
    }

    IEnumerator displayText(Collider2D other) {
        // Stop player movement during dialog
        float ogPlayerSpeed = other.gameObject.GetComponent<Player_Move_Update>().playerSpeed;
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = 0;
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(false);
        
        // Initial line of dialog
        message.text = "You've obtained a sword! ";

        //will continue after any key/button is clicked
        //copy the do-while block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "A blazing energy surges through you.";
        do {
        yield return null;
        } while (!Input.anyKeyDown);
        message.text = "You are unaccustomed to the tremendous weight of the sword. There is no way you could swing it while moving.";
        do {
        yield return null;
        } while (!Input.anyKeyDown);

        //let player move again
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(true);
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
        // Flag dialog as hit to prevent repetition
        hit=true;
    }
    
    void OnTriggerExit2D(Collider2D other) {
        // Reset text display
        if (other.gameObject.tag == "Player") {
            message.text = "";
        }
    }

    public bool IsNotHit() {
        return !hit;
    }
}
