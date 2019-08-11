﻿using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveControlsDialog : MonoBehaviour
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
        message.text = "Use arrow keys or WASD to move. Press SPACE to jump.";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        //while (!Input.anyKeyDown) {
        //yield return null;
        //}
        //

        //let player move again
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
