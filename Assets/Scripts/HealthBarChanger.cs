using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarChanger : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;

    public HealthBar HealthBar;
    void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.SetMaxhealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth);
    }
}
