using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _dashLayerMask;
    
    

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector3 _moveDir;
    private float _moveX;
    private float _moveY;
    [SerializeField]
    private float _dashAmount = 3f;
    [SerializeField]
    private float _dashCooldown = 2f;
    private float _dashCDTimer;
    private bool _isDashing = false;
    private bool _firstInput = false;
    private bool _secondInput = false;
    private int _currKeyPressed;
    private int _lastKeyPressed;
    private float _timeOfFirstButton;
    private bool _reset;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        _moveDir = new Vector3(_moveX, _moveY).normalized;
        
        _animator.SetFloat("Horizontal", _moveX);
        _animator.SetFloat("Vertical", _moveY);
        _animator.SetFloat("Speed", _moveDir.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.velocity = _moveDir * _moveSpeed;
        if (_isDashing)
        {
            Vector2 dashPosition = transform.position + _moveDir * _dashAmount;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _moveDir, _dashAmount, _dashLayerMask);
            if (hit.collider != null)
            {
                dashPosition = hit.point;
            }
            _rb.MovePosition(dashPosition);
            _isDashing = false;
        }
        //_rb.velocity = _moveDir * _moveSpeed;
        CheckForFlipping();
    }

    private void GetInputs()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        
        if (_dashCDTimer <= 0f)
        {
            if(_secondInput && _firstInput) 
            {
                if(Time.time - _timeOfFirstButton < 0.5f) {
                    Debug.Log("DoubleClicked");
                    _isDashing = true;
                } else {
                    Debug.Log("Too late");
                    _reset = true;
                }
 
                _reset = true;
            }
 
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!_firstInput)
                {
                    _lastKeyPressed = 1;
                    _firstInput = true;
                    _timeOfFirstButton = Time.time;
                }else
                {
                    _currKeyPressed = 1;
                    if (_currKeyPressed == _lastKeyPressed)
                    {
                        _secondInput = true;
                    }
                    else
                    {
                        _lastKeyPressed = 1;
                        _timeOfFirstButton = Time.time;
                    }
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!_firstInput)
                {
                    _lastKeyPressed = 2;
                    _firstInput = true;
                    _timeOfFirstButton = Time.time;
                }else
                {
                    _currKeyPressed = 2;
                    if (_currKeyPressed == _lastKeyPressed)
                    {
                        _secondInput = true;
                    }
                    else
                    {
                        _lastKeyPressed = 2;
                        _timeOfFirstButton = Time.time;
                    }
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!_firstInput)
                {
                    _lastKeyPressed = 3;
                    _firstInput = true;
                    _timeOfFirstButton = Time.time;
                }else
                {
                    _currKeyPressed = 3;
                    if (_currKeyPressed == _lastKeyPressed)
                    {
                        _secondInput = true;
                    }
                    else
                    {
                        _lastKeyPressed = 3;
                        _timeOfFirstButton = Time.time;
                    }
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!_firstInput)
                {
                    _lastKeyPressed = 4;
                    _firstInput = true;
                    _timeOfFirstButton = Time.time;
                }else
                {
                    _currKeyPressed = 4;
                    if (_currKeyPressed == _lastKeyPressed)
                    {
                        _secondInput = true;
                    }
                    else
                    {
                        _lastKeyPressed = 4;
                        _timeOfFirstButton = Time.time;
                    }
                    
                }
            }
 
            if(_reset) 
            {
                _firstInput = false;
                _secondInput = false;
                _reset = false;
                _dashCDTimer = _dashCooldown;
            }
        }
        else
        {
            _dashCDTimer -= Time.deltaTime;
        }
        
    }

    private void CheckForFlipping()
    {
        bool movingLeft = _moveX < 0;
        bool movingRight = _moveX > 0;

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
