using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
   [Header("Health")]
	public float maxHealth = 100;
	public float currentHealth;

    public SpriteRenderer playerSprite;

    float pingPong;

    Color originalColor;
    Color newColor;
    // public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        originalColor = new Color(1, 1, 1, 1);
        // healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     TakeDamage(20);
        // }
        playerSprite.color = originalColor;
        if (currentHealth == 0)
        {
            Debug.Log("DEAD");
        }
        
    }

    public void TakeDamage(float damage)
    {
        pingPong = Mathf.PingPong(Time.time, 0.1f);
        Debug.Log("Pingpong: " + pingPong);
        currentHealth -= damage;
        if (pingPong > 0.05)
        {
            playerSprite.color = originalColor;
        }
        else
        {
            playerSprite.color = Color.red;
        }
        // playerSprite.color = originalColor;


        // healthBar.SetHealth(currentHealth);
    }
}
