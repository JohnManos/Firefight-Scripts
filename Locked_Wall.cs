using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked_Wall : MonoBehaviour {

    public AudioClip soundEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if (collision.GetComponent<Player_Health_Update>().GetKeyCount() >= 1)
			{
				if (soundEffect)
    			{
       	 			AudioSource.PlayClipAtPoint(soundEffect, transform.position);
    			}
				Destroy(transform.parent.gameObject);
				collision.GetComponent<Player_Health_Update>().SubtractKey();
			}
		}
	}
}
