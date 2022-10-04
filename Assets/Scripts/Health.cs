using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Health : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int _strength = 15;
    public int _health = 100;
    [HideInInspector]
    public int MaxHealth;
    [SerializeField] private SimpleFlash _flash;
    public HealthBar HealthBar;
    private bool _isDying = false;

    private Vector3 _checkpoint;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField] 
    private AudioSource _deathAudio;
    [SerializeField]
    private AudioSource _damageAudio;
    [SerializeField]
    private Renderer _sRenderer;
    [SerializeField] 
    private int _dropChance = 10;
    [SerializeField] 
    private GameObject _droppedLoot;

    [SerializeField]
    private ParticleSystem _DamageParticles;

    public Animator myanimator;
    
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        MaxHealth = _health;
        _sRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "Player")
        {
            HealthBar.SetMaxhealth(MaxHealth);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Damage(10);
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
        _damageAudio.Play();
        myanimator.SetTrigger("TakeDamage");
        _DamageParticles.Play();
        this._health -= amount;
        _flash.Flash();
        
        if (gameObject.tag == "Player")
        {
            HealthBar.SetHealth(_health);
        }

        if(_health <= 0 && !_isDying)
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
        
        if (gameObject.tag == "Player")
        {
            HealthBar.SetHealth(_health);
        }
    }

    private void Die()
    {
        //Debug.Log("I am Dead!");
        if (gameObject.tag != "Player")
        {
            _isDying = true;
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
        _deathAudio.Play();
        myanimator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1.5f);
        _sRenderer.enabled = false;
        myanimator.SetBool("IsDead", false);
        yield return new WaitForSeconds(0.2f);
        transform.position = _checkpoint;
        _sRenderer.enabled = true;
        _health = MaxHealth;
        HealthBar.SetHealth(_health);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
    }

    private IEnumerator Destroy()
    {
        if (gameObject.tag == "Enemy")
        {
            _animator.SetTrigger("Die");
            EnemyDeathCount.KillCount++;
        }
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        _flash.Flash();
        yield return new WaitForSeconds(0.2f);
        if (Random.value < (_dropChance * 0.1f))
        {
            Instantiate(_droppedLoot, transform.position, Quaternion.identity);
        }
        //Debug.Log("Why not?");
        Destroy(gameObject);
    }
}
