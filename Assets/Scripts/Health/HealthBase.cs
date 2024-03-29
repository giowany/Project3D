using Animation;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    public List<UIUpdater> uiUpdater;

    public Action<HealthBase> onKill;
    public Action<HealthBase> onDamage;
    public Action<HealthBase> onRecover;

    [SerializeField] private float _currentLife;
    private bool _isDead = false;

    private void Awake()
    {
        ResetLife();
    }

    public float CurrentLife()
    {
        return _currentLife;
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        onRecover?.Invoke(this);
        UiUpdate();
        _isDead = false;
    }
    protected virtual void Kill()
    {
        _isDead = true;
        onKill?.Invoke(this);
        if (destroyOnKill)
            Destroy(gameObject, 3f);
    }

    public void OnDamage(float f, Vector3 dir)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }

        UiUpdate();
        onDamage?.Invoke(this);
        transform.DOMove(transform.position - dir, .1f);

    }

    public void Damage(float damage, Vector3 dir)
    {
        if(_isDead) return;
        OnDamage(damage, dir);
    }

    public void Damage(float damage)
    {
        throw new NotImplementedException();
    }

    private void UiUpdate()
    {
        if(uiUpdater != null)
            uiUpdater.ForEach(i => i.UpdateValue(startLife, _currentLife));
    }

    [NaughtyAttributes.Button]
    public void DamageDebug()
    {
        Damage(5f, Vector3.zero);
    }

    public void RecoverLife(int r)
    {
        _currentLife += r;
        onRecover?.Invoke(this);
        UiUpdate();
    }
}
