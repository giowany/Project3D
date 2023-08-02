using Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    public bool destroyOnKill = false;

    public Action<HealthBase> onKill;
    public Action<HealthBase> onDamage;

    [SerializeField] private float _currentLife;

    protected void ResetLife()
    {
        _currentLife = startLife;
    }
    protected virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, 3f);
    }

    [NaughtyAttributes.Button]
    private void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }

        onKill?.Invoke(this);
    }
}
