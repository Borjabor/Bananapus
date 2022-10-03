using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : HostileSpawner
{
    
    [SerializeField] 
    private float _distanceThreshold = 5f;

    [SerializeField]
    private float _moveSpeed = 3f;

    private Health _health;
    private Rigidbody2D _rb;

    private float _fleeDelay = 0.3f;
    private float _fleeTimer;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerPosition == null) return;
        float distance = Vector2.Distance(_playerPosition, transform.position);
        
        if (distance <= _distanceThreshold)
        {
            if (_fleeTimer >= _fleeDelay)
            {
                Vector3 direction = (_playerPosition - transform.position).normalized;
                transform.position -= direction * _moveSpeed * Time.deltaTime;
                if (distance >= _distanceThreshold)
                {
                    _fleeTimer = 0f;
                }
            }
            else
            {
                _fleeTimer += Time.deltaTime;
            }
        }else if (distance >= _spawnDistanceThreshold)
        {
            Vector3 direction = (_playerPosition - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }else if (_health._health <= 0)
        {
            transform.position = transform.position;
        }
        
        Spawn();
        CheckForFlipping();
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
