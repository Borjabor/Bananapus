using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileSpawner : MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenSpawns = 2f;
    private float _timer;
    [SerializeField] 
    private GameObject _spawnedEntity;
    [SerializeField] 
    private Transform _player;
    public static Vector3 _playerPosition;
    [SerializeField]
    protected float _spawnDistanceThreshold;
    [SerializeField] 
    private int _amountToSpawn;
    [SerializeField] private Animator _avocadoAnimator;



    void Update()
    {
        Spawn();
    }

    protected void Spawn()
    {
        _playerPosition = _player.position;
        _timer += Time.deltaTime;
        float distance = Vector2.Distance(_player.position, transform.position);
        if (distance < _spawnDistanceThreshold && _amountToSpawn > 0)
        {
            if (_timer >= _timeBetweenSpawns)
            {
                if (gameObject.tag == "Enemy")
                {
                    _avocadoAnimator.SetTrigger("RangedAttack");
                }
                Instantiate(_spawnedEntity, transform.position, Quaternion.identity);
                _amountToSpawn--;
                _timer = 0f;
            }
        }
    }
}
