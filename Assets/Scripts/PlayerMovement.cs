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
    private float _dashCooldown = 2f;
    private bool _isDashing;
    private bool _firstButtonPressed;
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
        
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_isDashing)
        {
            _rb.velocity = _moveDir * _moveSpeed;
        }else if (_isDashing)
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
        //CheckForFlipping();
    }

    private void GetInputs()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
        }

        
        if(Input.GetKeyDown(KeyCode.A) && _firstButtonPressed) {
            if(Time.time - _timeOfFirstButton < 0.5f) {
                Debug.Log("DoubleClicked");
                _isDashing = true;
            } else {
                Debug.Log("Too late");
                _isDashing = false;
            }
 
            _reset = true;
        }
 
        if(Input.GetKeyDown(KeyCode.A) && !_firstButtonPressed) {
            _firstButtonPressed = true;
            _timeOfFirstButton = Time.time;
        }
 
        if(_reset) {
            _firstButtonPressed = false;
            _reset = false;
        }
        
    }

    private void CheckForFlipping()
    {
        bool movingLeft = _movement.x < 0;
        bool movingRight = _movement.x > 0;

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
