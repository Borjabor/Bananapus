using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Health : MonoBehaviour
{
    public int _health = 100;
    [HideInInspector]
    public int MaxHealth;
    [SerializeField] private SimpleFlash _flash;

    private Vector3 _checkpoint;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField] 
    private AudioClip _deathAudio;
    [SerializeField]
    private Renderer _sRenderer;
    [SerializeField] 
    private int _dropChance = 10;
    [SerializeField] 
    private GameObject _droppedLoot;

    private void Awake()
    {
        MaxHealth = _health;
        _sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Damage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            // Heal(10);
        }
    }*/

    public void Damage(int amount)
    {
        if(amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this._health -= amount;
        _flash.Flash();

        if(_health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = _health + amount > MaxHealth;

        if (wouldBeOverMaxHealth)
        {
            this._health = MaxHealth;
        }
        else
        {
            this._health += amount;
        }
    }

    private void Die()
    {
        //Debug.Log("I am Dead!");
        if (gameObject.tag != "Player")
        {
            StartCoroutine(Destroy());
        }
        else
        {
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            _checkpoint = other.transform.position;
        }
    }

    private IEnumerator Respawn()
    {
        //_audioSource.PlayOneShot(_deathAudio);
        _sRenderer.enabled = false;
        Debug.Log($"got here");
        yield return new WaitForSeconds(1.5f);
        transform.position = _checkpoint;
        _sRenderer.enabled = true;
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        _health = MaxHealth;
    }

    private IEnumerator Destroy()
    {
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        if (Random.value < (_dropChance * 0.1f))
        {
            Instantiate(_droppedLoot, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
