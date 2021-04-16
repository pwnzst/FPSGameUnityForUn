using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public Slider slider;
    public GameObject healthBarUI;

    public void DeductHealth(float deductHealth)
    {
        enemyHealth -= deductHealth;
        slider.value -= deductHealth;
        if (enemyHealth <= 0) {
            EnemyDead();
            slider.value = 0;
           
        }
    }

  void Start()
    {
        slider.value = enemyHealth;
    }


    void EnemyDead()
    {
        Destroy(gameObject);
    }
 
  
}
