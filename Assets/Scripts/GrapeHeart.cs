using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeHeart : HostileSpawner
{
    private Health _health;
    void Start()
    {
        _health = gameObject.GetComponent<Health>();
    }
    void Update()
    {
        
    }

    public override void Spawn()
    {
        _playerPosition = _player.position;
        float distance = Vector2.Distance(_player.position, transform.position);
        Vector2 spawnLocation = transform.position + _playerPosition;
        Instantiate(_spawnedEntity, spawnLocation, Quaternion.identity);
    }

}
