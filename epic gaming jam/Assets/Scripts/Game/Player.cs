using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currenthealth;
    public int maxMp = 100;
    public int currentMp;

    public HealthBar healthBar;
    public MPSliderScript mpbar;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMp = maxMp;
        mpbar.SetMp(maxMp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamageMP(10);

        }
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
        
    }

    public void TakeDamageMP (int damage)
    {
        currentMp -= damage;
        mpbar.SetMp(currentMp);
    }
 
}
