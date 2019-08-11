using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerSpriteSwap_FireStaff : MonoBehaviour {

    private Text message;

	// Use this for initialization
	void Start () {
		message = GameObject.Find("Replay text").GetComponent<Text>();
        //message.color = textColor; //Color.white;
        message.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Swap()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_staff_sprite_512");
		//collision.transform.localScale += new Vector3(0.18f, 0.15f, 0);
		//collision.GetComponent<BoxCollider2D>().size = new Vector2(1.94792f, 3.301778f);
		Animator animator = player.GetComponent<Animator>();
		animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_staff_ANIMCTRL"); //Resources.Load("Assets/My Assets/player_armed_ANIMCTRL") as RuntimeAnimatorController;
		player.GetComponent<Weapon_Status>().SetWeapon("fireStaff"); 
        StartCoroutine(displayText());
    }

    IEnumerator displayText()
    {
        message.text = "Press R to replay level one with the fire staff.";
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        do {
        yield return null;
        } while (!Input.GetButtonDown("Replay"));

        message.text = "";
        SceneManager.LoadScene(4);
    }
}
