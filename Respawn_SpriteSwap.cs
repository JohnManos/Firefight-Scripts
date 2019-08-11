using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Respawn_SpriteSwap : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("Teleporter");
        }
    }
}
