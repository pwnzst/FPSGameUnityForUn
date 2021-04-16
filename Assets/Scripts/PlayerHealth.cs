using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead = false;
    public Text healthCounter;
    public Slider healthSlider;
    
    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
        UpdateHealthCounter();
    }

  

    public void DamagePlayer(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                Dead();
            } else { 
                currentHealth -= damage;
                healthSlider.value -= damage;
            }
            UpdateHealthCounter();
        }
       
    }

    void Dead()
    {
        currentHealth = 0;
        isDead = true;
        healthSlider.value = 0;
        Debug.Log("Умер");
    }

    void UpdateHealthCounter()
    {
        healthCounter.text = currentHealth.ToString();
    }
}
