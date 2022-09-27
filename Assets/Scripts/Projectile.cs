using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _target;
    private Vector2 _targetPosition;
    private Vector3 _normalizeDirection;
    private float _speed = 2f;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
