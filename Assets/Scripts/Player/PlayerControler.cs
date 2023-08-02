using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEditor;
using Animation;
using EBAC.Core.Singleton;

public class PlayerControler : MonoBehaviour, IDamageable
{
    #region Setups
    [Header("References")]
    public Transform tPlayer;
    public CharacterController player;
    public Animator animator;
    public SOPlayerConfig playerConfig;
    public string nameBool = "Run";
    public float gravity = -9.81f;
    public float startLife = 10;
    public AnimationBase animationBase;
    public List<FlashColor> flashColorList;

    private float _currentSpeed;
    private float _vSpeed;
    [SerializeField]private bool _isGrounded;
    private Vector3 velocity;
    private Inputs _inputs;
    private float _currLife;
    public bool _isDead = false;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.GamePlay.Enable();
        _currLife = startLife;
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
    public void HandleMovement()
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

        float axisX = Keyboard.current[Key.D].ReadValue() - Keyboard.current[Key.A].ReadValue();
        float axisZ = Keyboard.current[Key.W].ReadValue() - Keyboard.current[Key.S].ReadValue();

        playerConfig.moveDirection = new Vector3(0f, 0f, axisZ);
        playerConfig.moveDirection = transform.TransformDirection(playerConfig.moveDirection);
        playerConfig.moveDirection *= _currentSpeed;
        Jump();

        player.Move(playerConfig.moveDirection * Time.deltaTime);

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

    private void Kill()
    {
        OnKill();
        Destroy(gameObject, 2f);
    }

    private void OnKill()
    {
        _isDead = true;
        animationBase.PlayAnimationByType(AnimationType.DEATH);
    }

    private void OnDamage(float damage)
    {
        if (_isDead) return;
        flashColorList.ForEach(i => i.Flash());
        _currLife -= damage;
        if (_currLife <= 0f)
            Kill();
    }
    #endregion

    #region Interfaces
    public void Damage(float damage)
    {
        OnDamage(damage);
    }

    public void Damage(float damage, Vector3 dir)
    {
        transform.DOMove(transform.position - dir, .1f);
        OnDamage(damage);
    }
    #endregion
}
