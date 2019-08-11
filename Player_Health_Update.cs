using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health_Update : MonoBehaviour {


    // InstaDeath objects should be tagged "Death" and set as a trigger
    // Enemies (and other 1-damage obstacles) should be tagged "Enemy" and should NOT be set as a trigger

    private GameObject respawn = null; 
    private int health;
    private int playerScore;
    private int keyCount;
    private float timeLeft;
    private float timeSinceDamage;
    private float flickerTimer = 0;
    private float totalTimer = 0;
    private bool flickering = false;

    public string sceneAfterDeath;

    [Tooltip("The time limit for this level before the player dies.")]
    public float timeLimit = 500f;

    [Tooltip("The amount of hits a player can take before respawning.")]
    public static int maxHealth = 3;

    public float invincibilityBufferTime = 1f;


    [Tooltip("The score value of a coin or pickup.")]
    public int coinValue = 5;
    [Tooltip("The amount of points a player loses on death.")]
    public int deathPenalty = 20;
    public AudioClip scoreChirp;

    public Text scoreText;
    public Text keyText;
    public Text timeText;
    public Text healthText;


    // Use this for initialization
    void Start()
    {
        
        //respawn = GameObject.FindGameObjectWithTag("Respawn");

        health = maxHealth;
        playerScore = PlayerStats.GetPoints();
        keyCount = 0;
        timeLeft = timeLimit;

        PlayerStats.SetLastActiveSceneName(SceneManager.GetActiveScene().name);

        if (scoreText)
        {
            scoreText.text = "Score: " + playerScore.ToString("D2");
        }
    }

    void Update() 
    {
        timeLeft -= Time.deltaTime;
        if (timeText)
        {
            timeText.text = ("Time Left: " + (int)timeLeft);
        }
        if (timeLeft < 0.1f) {
            Die();
        } 

        timeSinceDamage += Time.deltaTime;

        if (flickering)
        {
            totalTimer += Time.deltaTime;
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= 0.06f)
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
                flickerTimer = 0f;
            }
            if (totalTimer >= 1f)
            {
                flickering = false;
                totalTimer = 0f;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }   

        if (healthText)
        {
            healthText.text = health + "/" + maxHealth;
        }
        if (keyText)
        {
            keyText.text = "Keys: " + keyCount.ToString("D2");
        }
        // Lowest Height death setting
        /*if (transform.position.y <= -13.8)
        {
            Die();
        }*/
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            if (respawn != null)
            {
                Respawn();
            }
            else
            {
                Die();
            }
        }
        else if (collision.CompareTag("Coin"))
        {
            AddPoints(coinValue);
            if (scoreChirp)
            {
                AudioSource.PlayClipAtPoint(scoreChirp, transform.position);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            if (collision.name == "key" || collision.name == "Key")
            {
                if (scoreChirp)
                {
                    AudioSource.PlayClipAtPoint(scoreChirp, transform.position);
                }
                ++keyCount;
            }
        }
        else if (collision.CompareTag("Respawn"))
        {
            respawn = collision.gameObject;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else if (collision.CompareTag("Finish"))
        {
            Time.timeScale = 0;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && timeSinceDamage >= invincibilityBufferTime)
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        timeSinceDamage = 0;
        flickering = true;
        //StartCoroutine(InjuredFlicker());
        if (--health == 0)
        {
            if (respawn != null)
            {
                Respawn();
            }
            else
            {
                Die();
            }
        }
    }

   /* IEnumerator InjuredFlicker()
    {
        while(true)
        {
            flickerTimer += Time.deltaTime;
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(.125f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(.125f);
        }
    }*/


    public void Respawn()
    {        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Enemy_Health enemyHealth = e.GetComponent(typeof(Enemy_Health)) as Enemy_Health;
            enemyHealth.SetCurrentHealth(enemyHealth.maxHealth);
        }
        
        health = maxHealth;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.transform.position = respawn.transform.position;
        AddPoints(-1*deathPenalty);
    }

    public void Die()
    {
        SceneManager.LoadScene(sceneAfterDeath);
    }

    public int GetScore()
    {
        return playerScore;
    }

    public int GetKeyCount()
    {
        return keyCount;
    }

    public void AddPoints(int amount)
    {
        playerScore += amount;
        PlayerStats.SetPoints(PlayerStats.GetPoints() + amount);
        scoreText.text = "Score: " + playerScore.ToString("D2");
    }

    public void SubtractKey()
    {
        --keyCount;
    }
}
