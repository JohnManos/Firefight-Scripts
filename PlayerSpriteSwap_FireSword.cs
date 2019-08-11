using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSpriteSwap_FireSword : MonoBehaviour {

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
            collision.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_armed_sprite_01_512");
			//collision.transform.localScale += new Vector3(0.18f, 0.15f, 0);
			//collision.GetComponent<BoxCollider2D>().size = new Vector2(1.94792f, 3.301778f);
			Animator animator = collision.GetComponent<Animator>();
			animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_armed_ANIMCTRL"); //Resources.Load("Assets/My Assets/player_armed_ANIMCTRL") as RuntimeAnimatorController;
			collision.GetComponent<Weapon_Status>().SetWeapon("fireSword"); 
        }
    }
}
