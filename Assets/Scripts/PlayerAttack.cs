using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    /*
     timer to check if the player pressed the attack button in time for combo
     timer that serves as a cooldown so the player cant spam the attack as fast they can
     check for hold (later)
     each attack detects targets and deals increasing damage
     */
    [SerializeField]
    private GameObject _attackArea01;
    [SerializeField]
    private GameObject _attackArea02;
    [SerializeField]
    private GameObject _attackArea03;

    public Animator _animator;
    

    private bool _attacking = false;
    private bool _hasAttacked = false;

    [SerializeField]
    private AudioSource _Attack1;
    [SerializeField]
    private AudioSource _Attack2;
    [SerializeField]
    private AudioSource _Attack3;


    private float _attackCooldown = 0.7f;
    private float _cooldownTimer;
    private float _comboTime = 0.5f;
    private float _comboTimer;
    private float _timeToAttack = 0.25f;
    //private float _timer = 0f;
    private int _comboCounter = 0;

    void Start()
    {
        _cooldownTimer = _attackCooldown;
    }

    void Update()
    {
        GetInputs();
        
        if(_hasAttacked)
        {
            //Debug.Log(Time.time - _comboTimer);
            if (Time.time - _comboTimer >= _comboTime)
            {
                _comboCounter = 0;
                _hasAttacked = false;
                _comboTimer = 0;
            }
        }

        /*if(_attacking)
        {
            _timer += Time.deltaTime;

            if(_timer >= _timeToAttack)
            {
                _timer = 0;
                _attacking = false;
                _attackArea01.SetActive(_attacking);
            }

        }*/
    }

    private void GetInputs()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !_attacking)
        {
            _comboCounter++;
            //Attack(_comboCounter);
            StartCoroutine(Attack(_comboCounter));
        }

    }

    /*private void Attack(int combo)
    {
        _attacking = true;
        _attackArea01.SetActive(_attacking);
    }*/

    private IEnumerator Attack(int combo)
    {
        _attacking = true;
        if (combo == 1)
        {
            _animator.SetTrigger("Attack1");
            _Attack1.Play();
            _attackArea01.SetActive(_attacking);
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
            _attackArea01.SetActive(_attacking);
            _comboTimer = Time.time;
            _hasAttacked = true;
        }else if(combo ==2)
        {
            _Attack2.Play();
            _attackArea02.SetActive(_attacking);
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
            _attackArea02.SetActive(_attacking);
            _comboTimer = Time.time;
            _hasAttacked = true;
        }else if (combo == 3)
        {
            _Attack3.Play();
            _attackArea03.SetActive(_attacking);
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
            _attackArea03.SetActive(_attacking);
            _comboTimer = Time.time;
            _hasAttacked = true;
        }else if (combo >= 4)
        {
            yield return new WaitForSeconds(_timeToAttack);
            _attacking = false;
            _comboCounter = 0;
        }
        
    }
}
