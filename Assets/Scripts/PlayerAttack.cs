using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    /*
     timer to check if the player pressed the attack button in time for combo
     timer that serves as a cooldown so the player cant spam the attack as fast they can
     check for hold (later)
     each attack detects targets and deals increasing damage
     */
    [SerializeField]
    private GameObject _attackArea01;
    [SerializeField]
    private GameObject _attackArea02;
    [SerializeField]
    private GameObject _attackArea03;
    

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                _attackArea01.SetActive(attacking);
            }

        }
    }

    private void Attack()
    {
        attacking = true;
        _attackArea01.SetActive(attacking);
    }
}
