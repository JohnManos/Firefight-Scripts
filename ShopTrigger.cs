using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour {

    /*[SerializeField] private GameObject displayed;
    void Start()
    {
        displayed.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ListenForButton());
            //displayed.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            displayed.SetActive(false);
        }
    }

    IEnumerator ListenForButton()
    {
        GameObject trigger = GameObject.Find("Talk Trigger");
        PushToTalkDialog dialogScript = trigger.GetComponent("PushToTalkDialog") as PushToTalkDialog;
        if (trigger != null && dialogScript != null)
        {
            do {
                yield return null;
            } while (dialogScript.GetNotHit() == true);
            displayed.SetActive(true);
            
            Debug.Log("connected.");
        }
    }*/
}
