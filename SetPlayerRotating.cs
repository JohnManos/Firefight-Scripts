using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerRotating : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<Player_Move_Update>().SetIsRotating(true);
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<Player_Move_Update>().SetIsRotating(false);
		}
	}
}
