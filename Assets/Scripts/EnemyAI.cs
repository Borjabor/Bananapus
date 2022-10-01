using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 _playerPosition;
    
    [SerializeField] 
    private float _chaseDistanceThreshold = 3f, _attackDistanceThreshold = 0.8f;

    [SerializeField] 
    private float _attackDelay = 1f;
    private float _passedTime = 1f;
    [SerializeField]
    private float _moveSpeed = 1f;

    [SerializeField]
    private GameObject _attackArea;
    private bool _attacking;
    private float _timeToAttack = 0.25f;
    private float _timer = 0f;
    private Health _health;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerPosition == null) return;

        _playerPosition = HostileSpawner._playerPosition;
        float distance = Vector2.Distance(_playerPosition, transform.position);
        
        if (distance <= _attackDistanceThreshold)
        {
            //Attack
            if (_passedTime >= _attackDelay)
            {
                _passedTime = 0f;
                Attack();
            }
        }else if (_health._health <= 0)
        {
            transform.position = transform.position;
        }
        else
        {
            //Chase
            //Debug.Log($"Chase");
            Vector3 direction = (_playerPosition - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
            //_rb.MovePosition(transform.position += direction * _moveSpeed * Time.deltaTime);
            //_rb.velocity = direction * _moveSpeed;
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
        
        CheckForFlipping();
    }

    private void Attack()
    {
        _attacking = true;
        _attackArea.SetActive(_attacking);
    }

    private void CheckForFlipping()
    {
        bool movingLeft = transform.position.x < _playerPosition.x;
        bool movingRight = transform.position.x > _playerPosition.x;

        if (movingLeft)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }

        if (movingRight)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y);
        }    
    }
}
