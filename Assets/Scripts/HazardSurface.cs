using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HazardSurface : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackArea;
    [SerializeField] 
    private float _attackDelay = 1f;
    private float _passedTime = 1f;
    private bool _attacking = false;
    private float _timeToAttack = 0.25f;
    private float _timer = 0f;
    private bool _playerInRange = false;

    
    private void Update()
    {
        if (_playerInRange)
        {
            if (_passedTime >= _attackDelay)
            {
                _passedTime = 0f;
                Attack();
            }
        
            if (_passedTime < _attackDelay)
            {
                _passedTime += Time.deltaTime;
            }
        
            if(_attacking)
            {
                _timer += Time.deltaTime;

                if(_timer >= _timeToAttack)
                {
                    _timer = 0;
                    _attacking = false;
                    _attackArea.SetActive(_attacking);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
    
    private void Attack()
    {
        _attacking = true;
        _attackArea.SetActive(_attacking);
    }

}
