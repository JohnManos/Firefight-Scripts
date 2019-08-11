using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PushToTalkDialog : MonoBehaviour
{
    //public Color textColor = Color.white;
    [SerializeField] private GameObject displayed;

    private Text message;
    private bool notHit = true;
    void Start()
    {
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Press To Talk").GetComponent<Text>();
        //message.color = textColor; //Color.white;
        message.text = "";
        notHit=true;
        displayed.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Player")
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

        message.text = "Push T to talk.";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.GetButtonDown("Talk"));
        
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = 0;
        other.gameObject.GetComponent<Player_Move_Update>().SetControl(false);

        message.text = "Greetings.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "It seems you have found several Pieces of Life.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "That is why I brought you here.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "I need them. Go forth and bring more to me.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "In exchange I offer you goods to support you on your journey.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "If you bring me enough, one day I will tell you of their significance.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "Let us exchange some Pieces of Life now.";

        do {
        yield return null;
        } while (!Input.anyKeyDown);

        message.text = "";
        notHit=false;
        displayed.SetActive(true);

        other.gameObject.GetComponent<Player_Move_Update>().SetControl(true);
        other.gameObject.GetComponent<Player_Move_Update>().playerSpeed = ogPlayerSpeed;
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
