using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    private float _timeBetweenShots = 2f;
    private float _timer;
    [SerializeField] 
    private GameObject _shot;
    [SerializeField] 
    private GameObject _player;
    public static Vector2 _playerPosition;
    
    
    
    void Update()
    {
        _playerPosition = _player.transform.position;
        _timer += Time.deltaTime;
        if (_timer >= _timeBetweenShots)
        {
            Instantiate(_shot, transform.position, Quaternion.identity);
            _timer = 0f;
        }

    }
}
