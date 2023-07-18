using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEditor;

public class PlayerControler : MonoBehaviour
{
    [Header("References")]
    public Transform tPlayer;
    public CharacterController player;
    public Animator animator;
    public SOPlayerConfig playerConfig;
    public string nameBool = "Run";
    public float gravity = -9.81f;

    private float _currentSpeed;
    private float _vSpeed;
    [SerializeField]private bool _isGrounded;
    private Vector3 velocity;
    private Inputs _inputs;

    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.GamePlay.Enable();
    }

    private void OnEnable()
    {
        if (_inputs != null)
            _inputs.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Disable();
    }

    public void HandleMovement()
    {
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            _currentSpeed = playerConfig.speed;
            animator.speed = playerConfig.speedWalkAnim;
        }

        else
        {
            _currentSpeed = playerConfig.speedRun;
            animator.speed = playerConfig.speedRunAnim;
        }

        float axisX = Keyboard.current[Key.D].ReadValue() - Keyboard.current[Key.A].ReadValue();
        float axisZ = Keyboard.current[Key.W].ReadValue() - Keyboard.current[Key.S].ReadValue();

        playerConfig.moveDirection = new Vector3(0f, 0f, axisZ);
        playerConfig.moveDirection = transform.TransformDirection(playerConfig.moveDirection);
        playerConfig.moveDirection *= _currentSpeed;
        Jump();

        player.Move(playerConfig.moveDirection * Time.deltaTime);

        /*var move = transform.forward * axisZ * _currentSpeed;
        var v = new Vector3(0, 0, 0);
        move.y -= gravit;

        player.Move(move * Time.deltaTime);*/
        transform.Rotate(0, axisX * Time.deltaTime * playerConfig.speedRotation, 0);
        animator.SetBool(nameBool, axisZ != 0);
    }

    private void Jump()
    {
        if (_isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            velocity.y = Mathf.Sqrt(playerConfig.jumpForce * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        if (_isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        _isGrounded = player.isGrounded;
        HandleMovement();
        ApplyGravity();
    }
}
