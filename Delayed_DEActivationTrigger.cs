using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delayed_DEActivationTrigger : MonoBehaviour {

    [SerializeField] private GameObject displayed;
    void Start()
    {
        displayed.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
           
                StartCoroutine(sendafuckingmessage(collision));
            
        }
    }
    IEnumerator sendafuckingmessage(Collider2D collision) 
    {
        //SendMessage("displayText", collision);
        do {
            yield return null;
        } while(GameObject.Find("Sword Trigger").GetComponent<SwordDialog>().getNotHit() == true);
        
            displayed.SetActive(false);
        
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            displayed.SetActive(true);
        }
    }*/
}
