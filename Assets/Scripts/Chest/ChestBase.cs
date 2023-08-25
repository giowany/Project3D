using Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBase : MonoBehaviour
{
    public AnimationBase animationBase;
    public List<ChestItenBase> chestItenList;
    public Transform itenPosition;


    private Inputs _inputs;
    [SerializeField] private bool _isAble = false;
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private ChestItenBase _curChestIten;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _isAble = false;
        _inputs = new Inputs();
        _inputs.GamePlay.Enable();
        _inputs.GamePlay.Interact.performed += ctx => OpenChest();

        _curChestIten = Instantiate(chestItenList[Random.Range(0, chestItenList.Count)], itenPosition);
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

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();

        if(player != null)
        {
            _isAble = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CloseChest();
        _isAble = false;
    }

    private void CloseChest()
    {
        if(!_isOpen) return;

        animationBase.PlayAnimationByType(AnimationType.CLOSE);
        _isOpen = false;
    }
    [NaughtyAttributes.Button]
    private void ChestItensColect()
    {
        _curChestIten.ShowItens();
    }

    public void OpenChest()
    {
        if (!_isAble) return;

        if (!_isOpen)
        {
            animationBase.PlayAnimationByType(AnimationType.OPEN);
            _isOpen = true;
            Invoke(nameof(ChestItensColect), 1f);
        }
        else if (_isOpen)
        {
            animationBase.PlayAnimationByType(AnimationType.CLOSE);
            _isOpen= false;
        }
    }
}
