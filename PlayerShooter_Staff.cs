using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter_Staff : MonoBehaviour {

	[SerializeField] private GameObject projectile;
    private bool facingRight = true;
    [SerializeField] private int MaxActiveShots;
    private int currentActiveShots = 0;
    private GameObject player;
    private Rigidbody2D playerRigidBody;
	private float timeSinceLastShot;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastShot += Time.deltaTime;
		if (Input.GetButtonDown("Fire1") && player.GetComponent<Player_Move_Update>().GetControl() == true)
        {   
			if (player.GetComponent<Weapon_Status>().GetWeapon() == "fireStaff")
            {
                if (currentActiveShots < MaxActiveShots)
                {
					timeSinceLastShot = 0;
                    ++currentActiveShots;
                    StartCoroutine(Shot());
                    
                }
            }
		}
		if (timeSinceLastShot > 1.3f)
        {
            currentActiveShots = 0;
        }
	}

	public void DecrementActiveShots()
    {
        Debug.Log("we decremented. " + currentActiveShots + " prior to decrement.");
		if (currentActiveShots != 0)
		{
        	--currentActiveShots;
		}
        Debug.Log("we decremented. " + currentActiveShots + " after decrement.");
    }

    IEnumerator Shot()
    {
        GameObject shot = Instantiate(projectile, transform.position + new Vector3(1,1,0), transform.rotation);
        shot.SetActive(true);
        float playerSpeed = playerRigidBody.velocity.x;
        shot.GetComponent<Projectile>().Launch(playerSpeed, !playerRigidBody.GetComponent<SpriteRenderer>().flipX);
        return null;
    }
}
