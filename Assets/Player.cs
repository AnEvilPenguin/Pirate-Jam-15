using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float Speed = 1.0f;

    private Vector2 _moveInput;
    private InputAction _movement;
    private PlayerActions _playerActions;

    void Awake()
    {
        _playerActions = new PlayerActions();
        _movement = _playerActions.Player_Map.Movement;
    }

    void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _moveInput = _movement.ReadValue<Vector2>();
        Rigidbody.velocity = _moveInput * Speed;
    }
}
