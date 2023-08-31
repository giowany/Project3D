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
using Cinemachine;
using Skins;

public class PlayerControler : Singleton<PlayerControler>
{
    #region Setups
    [Header("References")]
    public Transform tPlayer;
    public CharacterController player;
    public SOPlayerConfig playerConfig;
    public List<FlashColor> flashColorList;
    public HealthBase healthBase;
    public GameObject pRotation;
    public List<Collider> colliders;
    public ShakeCamera shakeCamera;
    public EffectsManager effectsManager;

    [Header("Atributs")]
    public float gravity = -9.81f;
    public bool isDead = false;

    [Header("Animation")]
    public Animator animator;
    public string nameBool = "Run";
    public AnimationBase animationBase;

    private float _currentSpeed;
    private float _vSpeed;
    private bool _isGrounded;
    private Vector3 velocity;
    private Inputs _inputs;
    private Vector3 _moveInput;
    private bool _isRuning;
    private bool _isJumping;
    private Transform _cameraTransform;
    private float _playerRotation = 0f;
    private Vector3 _playerInit;
    #endregion

    #region Unity Functions
    private void Start()
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

        _inputs.GamePlay.Run.started += ctx => _isRuning = true;
        _inputs.GamePlay.Run.canceled += ctx => _isRuning = false;
        _inputs.GamePlay.Jump.started += ctx => _isJumping = true;
        _inputs.GamePlay.Jump.canceled += ctx => _isJumping = false;

        healthBase.onDamage += OnDamage;
        healthBase.onKill += OnKill;
        healthBase.onRecover += OnRecover;

        _cameraTransform = Camera.main.transform;

        _playerInit = transform.position;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void HandleMovement()
    {
        if(isDead) return;

        if (!_isRuning)
        {
            _currentSpeed = playerConfig.speed;
            ChangeSpeed();
            animator.speed = playerConfig.speedWalkAnim;
        }

        else
        {
            _currentSpeed = playerConfig.speedRun;
            ChangeSpeed();
            animator.speed = playerConfig.speedRunAnim;
        }

        _moveInput = _inputs.GamePlay.Move.ReadValue<Vector3>();
        Vector2 rotate = _inputs.GamePlay.RHorizontal.ReadValue<Vector2>();

        RotatePlayer(_moveInput);
        _moveInput = transform.TransformDirection(_moveInput);
        _moveInput *= _currentSpeed;
        Jump();

        player.Move(_moveInput * Time.deltaTime);

        transform.Rotate(0f, rotate.x * Time.deltaTime * playerConfig.speedRotationCamera, 0f);

        animationBase.AnimationBool(AnimationType.RUN, _moveInput.z != 0f || _moveInput.x != 0f);
    }

    private void RotatePlayer(Vector3 moveInput)
    {
        if (moveInput != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg;
            _playerRotation = Mathf.SmoothDampAngle(_playerRotation, targetRotation + _cameraTransform.eulerAngles.y, ref playerConfig.speedRotation, 0.1f);
            pRotation.transform.rotation = Quaternion.Euler(0f, _playerRotation, 0f);
        }
    }

    private void Jump()
    {
        if (_isGrounded && _isJumping)
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

    public void Respawn()
    {
        if (CheckPointManager.instance.HasCheckPoint())
        {
            transform.position = CheckPointManager.instance.GetPositionLastCheckPoint();
            OnRespawn();
        }

        else
        {
            transform.position = _playerInit;
            OnRespawn();
        }
    }

    private void OnRespawn()
    {
        healthBase.ResetLife();
        isDead = false;
        colliders.ForEach(collider => collider.enabled = true);
        animationBase.PlayAnimationByType(AnimationType.IDLE);
        effectsManager.ChangeVignette(0f);
    }

    public bool Isgrounded()
    {
        return _isGrounded;
    }
    #endregion

    #region Healt Player
    private void OnKill(HealthBase health)
    {
        isDead = true;
        colliders.ForEach(collider => collider.enabled = false);
        animationBase.PlayAnimationByType(AnimationType.DEATH);
        Invoke(nameof(Respawn), 5f);
    }

    private void OnDamage(HealthBase health)
    {
        if (isDead) return;
        flashColorList.ForEach(i => i.Flash());
        shakeCamera.Shake();
        effectsManager.ChangeVignette((1f - (health.CurrentLife() / health.startLife)) / 2f);
    }

    private void OnRecover(HealthBase health)
    {
        effectsManager.ChangeVignette(0f);
    }
    #endregion

    #region Cloth Skils
    public void ChangeSpeed()
    {
        _currentSpeed += ClothManager.instance.BonusSpeed();
    }
    #endregion
}
