using DG.Tweening;
using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItenBase : MonoBehaviour
{
    public int itenNumb = 5;
    public GameObject itenObj;
    public Vector2 itenRandon = new Vector2(-.7f, .7f);
    public ItenType itenType;

    [Header("Animation Setup")]
    public float timeDuration = .7f;
    public Ease ease = Ease.OutBack;

    protected List<GameObject> _itenList = new List<GameObject>();
    [SerializeField] private bool _isCollected = false;

    public virtual void Collect()
    {
        if (_isCollected) return;

        foreach (var i in _itenList)
        {
            i.transform.DOMoveY(2f, timeDuration).SetRelative();
            i.transform.DOScale(0, timeDuration / 2).SetDelay(timeDuration / 2);
            InvetoryManager.instance.AddItensForType(itenType);
            LayoutManager.instance.UpdateUI(itenType);
        }

        _isCollected = true;
    }

    public virtual void ShowItens()
    {
        if (_isCollected) return;

        for (int i = 0; i < itenNumb; i++)
        {
            var iten = Instantiate(itenObj);
            iten.transform.position = transform.position + Vector3.forward * Random.Range(itenRandon.x, itenRandon.y) + Vector3.right * Random.Range(itenRandon.x, itenRandon.y);
            iten.transform.DOScale(0, timeDuration).SetEase(ease).From();
            _itenList.Add(iten);
        }

        Collect();
    }
}
