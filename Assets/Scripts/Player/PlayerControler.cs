using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerControler : MonoBehaviour
{
    [Header("References")]
    public Transform tPlayer;
    public CharacterController player;
    public Animator animator;
    public SOPlayerConfig playerConfig;
    public string nameBool = "Run";
    public float gravit = 9.81f;

    private float _currentSpeed;
    private float _vSpeed;
    [SerializeField]private bool _isGrounded;

    public void HandleMovement()
    {
        if (!Input.GetButton("Run"))
        {
            _currentSpeed = playerConfig.speed;
            animator.speed = playerConfig.speedWalkAnim;
        }

        else
        {
            _currentSpeed = playerConfig.speedRun;
            animator.speed = playerConfig.speedRunAnim;
        }

        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        playerConfig.moveDirection = new Vector3(0f, 0f, axisZ);
        playerConfig.moveDirection = transform.TransformDirection(playerConfig.moveDirection);
        playerConfig.moveDirection *= _currentSpeed;
        _vSpeed = gravit;
        playerConfig.moveDirection.y -= _vSpeed;
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
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            playerConfig.moveDirection.y += playerConfig.jumpForce;
        }
    }

    private void Update()
    {
        _isGrounded = player.isGrounded;
        HandleMovement();
    }
}
