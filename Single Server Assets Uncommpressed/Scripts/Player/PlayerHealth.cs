using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxShield = 100;
    public float maxHealth = 100;
    public float currentShield;
    public float currentHealth;

    public bool shieldActive = true;

    public Text shieldText;
    public Text healthText;
    void Start()
    {
        currentShield = maxShield;
        currentHealth = maxHealth;
        shieldActive = true;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(currentShield > 0)
            currentShield -= damage;

        if(currentShield <= 0)
            shieldActive = false;

        shieldText.text = "S:" + currentShield.ToString();
        if(shieldActive == false)
        {
            currentHealth -= damage;
            healthText.text = "H:" + currentHealth.ToString();
            if(currentHealth <= 0)
                Die();
        }
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 
}
