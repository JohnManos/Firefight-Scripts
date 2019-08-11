using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour {

	public int maxHealth = 3;
	public Text healthText;
	public GameObject hitEffect;
	public GameObject[] drops;
	private bool hitEffectActive = false;
	private float hitEffectTimer = 0f;
	private bool healthTextActive = false;
	private float healthTextTimer = 0f;
	private int currentHealth;
	

	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (gameObject.transform.position.y < -100)
		{
            Destroy(gameObject);
        }
		if (hitEffectActive == true)
		{
			hitEffectTimer += Time.deltaTime;
		}
		if (hitEffectTimer == 0.35f)
		{
			hitEffect.SetActive(false);
			hitEffectActive = false;
			hitEffectTimer = 0f;
		}
		if (healthTextActive == true)
		{
			healthTextTimer += Time.deltaTime;
		}
		if (healthTextTimer >= 2f)
		{
			healthText.text = "";
			//healthText.transform.parent.gameObject.SetActive(false);
			healthTextActive = false;
			healthTextTimer = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("playerProjectile1"))
		{
			if (--currentHealth <= 0)
			{
				Destroy(gameObject);
				foreach (GameObject i in drops)
				{
					i.SetActive(true);
				}
			}
			
			if (hitEffect)
			{
				hitEffect.SetActive(true);
				hitEffect.GetComponent<ParticleSystem>().Simulate(0.0f, true, true);
				hitEffect.GetComponent<ParticleSystem>().Play();
				hitEffectActive = true;
			}

			if (healthText)
			{
				//healthText.transform.parent.gameObject.SetActive(true);
				healthTextActive = true;
				healthText.text = currentHealth.ToString("D2");
			}
			
		}
		if (collision.CompareTag("playerProjectile2"))
		{
			currentHealth = currentHealth - 2;
			if (currentHealth <= 0)
			{
				Destroy(gameObject);
				foreach (GameObject i in drops)
				{
					i.SetActive(true);
				}
			}
			
			if (hitEffect)
			{
				hitEffect.SetActive(true);
				hitEffect.GetComponent<ParticleSystem>().Simulate(0.0f, true, true);
				hitEffect.GetComponent<ParticleSystem>().Play();
				hitEffectActive = true;
			}

			if (healthText)
			{
				//healthText.transform.parent.gameObject.SetActive(true);
				healthTextActive = true;
				healthText.text = currentHealth.ToString("D2");
			}
			
		}
	}

	public void SetCurrentHealth(int newHealth)
	{
		currentHealth = newHealth;
	}
}
