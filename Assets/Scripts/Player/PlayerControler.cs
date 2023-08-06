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
    public GameObject pRotation;
    public List<Collider> colliders;

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

        if (_inputs.GamePlay.Run.ReadValue<float>() == 0)
        {
            _currentSpeed = playerConfig.speed;
            animator.speed = playerConfig.speedWalkAnim;
        }

        else
        {
            _currentSpeed = playerConfig.speedRun;
            animator.speed = playerConfig.speedRunAnim;
        }

        Vector3 move = _inputs.GamePlay.Move.ReadValue<Vector3>();
        Vector2 rotate = _inputs.GamePlay.RHorizontal.ReadValue<Vector2>();

        RotatePlayer(move.x, move.z);
        move = transform.TransformDirection(move);
        move *= _currentSpeed;
        Jump();

        player.Move(move * Time.deltaTime);

        transform.Rotate(0, rotate.x * Time.deltaTime * playerConfig.speedRotation, 0);

        animator.SetBool(nameBool, move.z != 0 || move.x != 0);
    }

    private void RotatePlayer(float x, float z)
    {
        if (x != 0) pRotation.transform.DOLocalRotate(new Vector3(0, 90 * x, 0), .1f);
        else if (z == -1) pRotation.transform.DOLocalRotate(new Vector3(0, 180 * z, 0), .1f);
        else if(z == 1) pRotation.transform.DOLocalRotate(new Vector3(0, z, 0), .1f);

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

    public void Respawn()
    {
        if (CheckPointManager.instance.HasCheckPoint())
        {
            transform.position = CheckPointManager.instance.GetPositionLastCheckPoint();
            healthBase.ResetLife();
            _isDead = false;
            colliders.ForEach(collider => collider.enabled = true);
            animationBase.PlayAnimationByType(AnimationType.IDLE);
        }
    }
    #endregion

    #region Healt Player
    private void OnKill(HealthBase health)
    {
        _isDead = true;
        colliders.ForEach(collider => collider.enabled = false);
        animationBase.PlayAnimationByType(AnimationType.DEATH);
        Invoke(nameof(Respawn), 2);
    }

    private void OnDamage(HealthBase health)
    {
        if (_isDead) return;
        flashColorList.ForEach(i => i.Flash());
    }
    #endregion
}
