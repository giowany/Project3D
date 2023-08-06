using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEditor;
using Animation;
using EBAC.Core.Singleton;
using System.Linq;

public class PlayerControler : MonoBehaviour
{
    #region Setups
    [Header("References")]
    public Transform tPlayer;
    public CharacterController player;
    public Animator animator;
    public SOPlayerConfig playerConfig;
    public string nameBool = "Run";
    public float gravity = -9.81f;
    public AnimationBase animationBase;
    public List<FlashColor> flashColorList;
    public HealthBase healthBase;

    private float _currentSpeed;
    private float _vSpeed;
    [SerializeField]private bool _isGrounded;
    private Vector3 velocity;
    private Inputs _inputs;
    public bool _isDead = false;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        Init();
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
    private void Update()
    {
        _isGrounded = player.isGrounded;
        HandleMovement();
        ApplyGravity();
    }
    #endregion

    #region Controller
    private void Init()
    {
        _inputs = new Inputs();
        _inputs.GamePlay.Enable();

        healthBase.onDamage += OnDamage;
        healthBase.onKill += OnKill;
    }

    private void HandleMovement()
    {
        if(_isDead) return;

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

        Vector2 move1 = _inputs.GamePlay.Move.ReadValue<Vector2>();

        playerConfig.moveDirection = new Vector3(0f, 0f, move1.y);
        playerConfig.moveDirection = transform.TransformDirection(playerConfig.moveDirection);
        playerConfig.moveDirection *= _currentSpeed;
        Jump();

        player.Move(playerConfig.moveDirection * Time.deltaTime);

        transform.Rotate(0, move1.x * Time.deltaTime * playerConfig.speedRotation, 0);
        animator.SetBool(nameBool, move1.y != 0);
    }

    private void Jump()
    {
        if (_isGrounded && _inputs.GamePlay.Jump.ReadValue<float>() == 1)
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
    #endregion

    #region Healt Player
    private void OnKill(HealthBase health)
    {
        _isDead = true;
        animationBase.PlayAnimationByType(AnimationType.DEATH);
    }

    private void OnDamage(HealthBase health)
    {
        if (_isDead) return;
        flashColorList.ForEach(i => i.Flash());
    }
    #endregion
}
