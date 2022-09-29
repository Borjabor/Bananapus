using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _target;
    private Vector2 _targetPosition;
    private Vector3 _normalizeDirection;
    [SerializeField]
    private float _speed = 2f;

    [SerializeField] private int  _damage;
    
    private float _timeToDestroy = 7f;
    private float _timer;

    private void Start()
    {
        _targetPosition = ShootProjectile._playerPosition;
        _normalizeDirection = (_targetPosition - (Vector2)transform.position).normalized;
    }

    private void Update()
    {
        float step = _speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);
        transform.position += _normalizeDirection * _speed * Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= _timeToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Health>() != null && other.gameObject.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            //Debug.Log($"hit");
            health.Damage(_damage);
        }
        
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
