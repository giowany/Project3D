using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItens : MonoBehaviour
{
    public HealthBase health;
    public float shakeDuration = .1f;
    public int shakeForce = 1;
    public float force = 1f;
    public Collider colliderIten;

    [Header("Itens")]
    public int dropItenAmout = 10;
    public GameObject iten;
    public Transform itenPosition;
    public float timeBetweenSpaw = .2f;

    [Header("Animation")]
    public float timeAnimation = .2f;
    public Ease ease;

    private void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
        if(colliderIten == null) colliderIten = GetComponent<Collider>();
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        OnValidate();
        health.onDamage += OnDamage;
        health.onKill += OnKill;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up / force, shakeForce);
        DropIten();
    }

    private void OnKill(HealthBase h)
    {
        colliderIten.enabled = false;
        DropGrupOfItens();
    }

    private void DropIten()
    {
        var i = Instantiate(iten);
        i.transform.position = itenPosition.position;
        i.transform.DOScale(0, timeAnimation).SetEase(ease).From();
    }

    private void DropGrupOfItens()
    {
        StartCoroutine(DropGrupOfItensCorotine());
    }

    private IEnumerator DropGrupOfItensCorotine()
    {
        for (int i = 0; i < dropItenAmout; i++)
        {
            DropIten();
            yield return new WaitForSeconds(timeBetweenSpaw);
        }
    }
}
