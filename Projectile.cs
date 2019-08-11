using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public AudioClip soundClip;
    [SerializeField] private float lifeTime = 2.0f;
    private int lifeTimeFrames = 0;
    private float timeLived = 0;


    [SerializeField] private float relativeVelocity = 1f;
    private float velocity = 0f;

    private PlayerShooter_Sword swordShooter;
    private PlayerShooter_Staff staffShooter;
    private bool isSwordShot = false;
    private bool isStaffShot = false;


	void Start () {
        swordShooter = GameObject.FindWithTag("Player").GetComponent("PlayerShooter_Sword") as PlayerShooter_Sword;
        if (swordShooter.enabled)
        {
            isSwordShot = true;
        }
        staffShooter = GameObject.FindWithTag("Player").GetComponent("PlayerShooter_Staff") as PlayerShooter_Staff;
        if (staffShooter.enabled)
        {
            isStaffShot = true;
        }
        lifeTimeFrames = (int)(lifeTime / Time.fixedDeltaTime);
	}
	
    public void Launch(float emitVelocity, bool goingRight)
    {
        AudioSource.PlayClipAtPoint(soundClip, transform.position);
        if (goingRight)
        {
            velocity = emitVelocity + relativeVelocity;
        }
        else
        {
            velocity = emitVelocity - relativeVelocity;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * velocity;
    }
	void FixedUpdate () {
		if (timeLived < lifeTime) //if (timeLived < lifeTimeFrames)
        {
            timeLived += Time.fixedDeltaTime;//timeLived++;
        }
        else
        {
            if (isSwordShot)
            {
            swordShooter.DecrementActiveShots();
            }
            else if (isStaffShot)
            {
                staffShooter.DecrementActiveShots();
            }
            Destroy(gameObject);
        }
	}
}
