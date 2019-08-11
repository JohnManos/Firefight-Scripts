using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter_Sword : MonoBehaviour {


    [SerializeField] private GameObject projectile;
    private bool facingRight = true;
    [SerializeField] private int MaxActiveShots;
    private int currentActiveShots = 0;
    private GameObject player;
    private Rigidbody2D playerRigidBody;
    private float timeSinceLastShot;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        player.GetComponent<Animator>().SetBool("IsSlicing", false);    
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && player.GetComponent<Player_Move_Update>().GetControl() == true)
        {   
            //Debug.Log("We initiated.");
            if (player.GetComponent<Weapon_Status>().GetWeapon() == "fireSword")
            {
                //Debug.Log("Weapon detected.");
                if (player.GetComponent<Player_Move_Update>().GetIsMoving() == false)
                {
                    //Debug.Log("We are not moving." + currentActiveShots);
                    if (currentActiveShots < MaxActiveShots)
                    {
                        timeSinceLastShot = 0;
                        Debug.Log("Current shots < max.");
                        player.GetComponent<Animator>().SetBool("IsRunning", false);
                        ++currentActiveShots;
                        Debug.Log("We incremented the shots.");
                        StartCoroutine(Animate_Shot());
                        
                    }
                }
            }
        }
        if (timeSinceLastShot > 2f)
        {
            currentActiveShots = 0;
        }
    }

    IEnumerator Animate_Shot()
    {
        player.GetComponent<Animator>().SetBool("IsSlicing", true);
        yield return new WaitForSeconds(.5f);
        GameObject shot = Instantiate(projectile, transform.position + new Vector3(1,1,0), transform.rotation);
        if (playerRigidBody.GetComponent<SpriteRenderer>().flipX) 
        {
            shot.GetComponent<SpriteRenderer>().flipX = true;
        }
        shot.SetActive(true);
        float playerSpeed = playerRigidBody.velocity.x;
        shot.GetComponent<Projectile>().Launch(playerSpeed, !playerRigidBody.GetComponent<SpriteRenderer>().flipX);
        yield return new WaitForSeconds(.5f);
        player.GetComponent<Animator>().SetBool("IsSlicing", false);
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
