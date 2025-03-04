using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private Vector3 _velocity;
    private float _speed = 5f;
    private float _jumpHeight = 2f;
    private float _gravity = -9.81f;
    private bool _isJumping;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();

        // InputActions�̎擾
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
    }

    private void Update()
    {
        // �ړ�����
        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        _controller.Move(move * _speed * Time.deltaTime);

        // �A�j���[�V�����̐ݒ�
        _animator.SetFloat("Speed", move.magnitude);

        // �W�����v����
        if (_jumpAction.triggered && !_isJumping)
        {
            _isJumping = true;
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _animator.SetTrigger("Jump");
        }

        // �d�͂̓K�p
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
            _isJumping = false;
        }
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}

