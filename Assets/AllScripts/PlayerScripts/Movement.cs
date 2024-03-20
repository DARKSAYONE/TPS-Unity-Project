using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Important Components")]
    [SerializeField] private PlayerStats Stat;
    [SerializeField] private CharacterController ChControl;
    [SerializeField] private AnimationControl _anim;
    [Header("Flags")]
    [SerializeField] public bool CanMove = true;
    [SerializeField] public bool isJump = false;

    [Header("Other")]
    [SerializeField] public Vector3 _moveVector;
    
    private float _fallVelocity = 0f;
    private float _gravity = 9.8f;

    void Start()
    {
       InitComponents();
    }

    void Update()
    {
        GetMoveInput();
        if (Input.GetKey(KeyCode.Space) && CanMove && ChControl.isGrounded)
            Jump();

        if (ChControl.isGrounded)
            isJump = false;
        else if (!ChControl.isGrounded)
            isJump = true;
    }

    private void FixedUpdate()
    {
        PlayerFall();
        Move();
    }

    private void GetMoveInput()
    {
        _moveVector = Vector3.zero;
        _anim.Moving = 0;
        if (Input.GetKey(KeyCode.W) && CanMove)
        {
            _moveVector += transform.forward;
            _anim.Moving = 1;
        }
        else if (Input.GetKey(KeyCode.S) && CanMove)
        {
            _anim.Moving = -1;
            _moveVector -= transform.forward;
        }
        else if (Input.GetKey(KeyCode.D) && CanMove)
        {
            _moveVector += transform.right;
            _anim.Moving = 2;
        }
        else if (Input.GetKey(KeyCode.A) && CanMove)
        {
            _moveVector -= transform.right;
            _anim.Moving = 3;
        }

    }
    private void Move()
    {
        ChControl.Move(_moveVector * Stat.MoveSpeed * Time.fixedDeltaTime);
    }
    
    void PlayerFall()
    {
        _fallVelocity += _gravity * Time.fixedDeltaTime;
        ChControl.Move(Vector3.down * _fallVelocity * Time.fixedDeltaTime);
        if (ChControl.isGrounded)
            _fallVelocity = 0.0f;
    }
    void Jump()
    {
        _fallVelocity = -Stat.JumpForce;
    }

    void InitComponents()
    {
        ChControl = GetComponent<CharacterController>();
        if (ChControl == null)
            Debug.LogError("Character Controller not found");
        Stat = GetComponent<PlayerStats>();
        if (Stat == null)
            Debug.LogError("Player Stats not found");
        _anim = GetComponentInChildren<AnimationControl>();
        if (_anim == null)
            Debug.LogError("Animation Control not found");
    }
}
