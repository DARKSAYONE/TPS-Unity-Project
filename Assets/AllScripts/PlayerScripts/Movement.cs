using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Important Components")]
    [SerializeField] private PlayerStats Stat;
    [SerializeField] private CharacterController ChControl;
    [Header("Flags")]
    [SerializeField] public bool CanMove = true;

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
    }

    private void FixedUpdate()
    {
        PlayerFall();
        Move();
    }

    private void GetMoveInput()
    {
        _moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W) && CanMove)
            _moveVector += transform.forward;
        if(Input.GetKey(KeyCode.S) && CanMove)
            _moveVector -= transform.forward;
        if (Input.GetKey(KeyCode.D) && CanMove)
            _moveVector += transform.right;
        if (Input.GetKey(KeyCode.A) && CanMove)
            _moveVector -= transform.right;

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
    }
}
