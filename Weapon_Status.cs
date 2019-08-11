using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Status : MonoBehaviour {
	public bool hasSword = false;
	public bool hasStaff = false;

	private string currentWeapon;
	private string[] weapons;
	private int weaponIndex = 0;
	private PlayerShooter_Sword swordScript;
	private PlayerShooter_Staff staffScript;

	// Use this for initialization
	void Start () {
		swordScript = GetComponent("PlayerShooter_Sword") as PlayerShooter_Sword;
		staffScript = GetComponent("PlayerShooter_Staff") as PlayerShooter_Staff;

		// TODO: The following logic is not very scalable. 
		// An additional condition for each possible weapon-possession combination
		// is only feasible for this demo (in which there are only two weapons)
		if (hasSword == false || hasStaff == false) {
			currentWeapon = "none";
			weapons = null;
		}
		else if (hasSword == true && hasStaff == false) {
			// Set the current weapon and the array of possessed weapons
			currentWeapon = "fireSword";
			weapons = new string[] {"fireSword"};
			// Set the player sprite and its animation based on the weapon held
			GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_armed_sprite_01_512");
			Animator animator = GetComponent<Animator>();
			animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_armed_ANIMCTRL");
		}
		else if (hasStaff) { // in this case, if player has the staff he must have already obtained the sword
			// Set the current weapon and the array of possessed weapons
			currentWeapon = "fireStaff";
			weapons = new string[] {"fireSword", "fireStaff"};
			weaponIndex = 1;
			// Set the player sprite and its animation based on the weapon held
			GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_staff_sprite_512");
			Animator animator = GetComponent<Animator>();
			animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_staff_ANIMCTRL");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetButtonDown("Switch")) {
			//Debug.Log("Weapon switche to index:" + weaponIndex);
			if (weapons != null) {
				// Increment to next weapon in array, wrapping around
				if (++weaponIndex > weapons.Length - 1) {
					weaponIndex = 0;
				}			
				currentWeapon = weapons[weaponIndex];
			}

			if (currentWeapon == "fireSword") {
				swordScript.enabled = true;
				staffScript.enabled = false;
				GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_armed_sprite_01_512");
				Animator animator = GetComponent<Animator>();
				animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_armed_ANIMCTRL");
			}
			else if (currentWeapon == "fireStaff") {
				swordScript.enabled = false;
				staffScript.enabled = true;
				GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load<Sprite>("player_staff_sprite_512");
				Animator animator = GetComponent<Animator>();
				animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("player_staff_ANIMCTRL");
			}
		}
	}

	// Be careful using this function, it is not fully supported as is (think about the weapon array index)
	public void SetWeapon(string weapon) {
		currentWeapon = weapon;
	}

	public string GetWeapon() {
		return currentWeapon;
	}
}
