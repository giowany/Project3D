using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public float damageAmout = 1f;
    public float speed = 50f;
    public List<string> tagsToHit;

    private Vector3 _dir = Vector3.zero;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {

        foreach (var tag in tagsToHit)
        {
            if (collision.transform.CompareTag(tag))
            {
                if (_particleSystem != null)
                {
                    _particleSystem.transform.SetParent(null);
                    _particleSystem.Play();
                }
                var damageble = collision.transform.GetComponent<IDamageable>();
                if (damageble != null)
                {
                    if (collision.transform.CompareTag(tagsToHit[0]))
                    {
                        _dir = collision.transform.position - transform.position;
                        _dir = -_dir.normalized;
                        _dir.y = 0;
                        _particleSystem.Play();

                    }

                    _particleSystem.Play();
                    damageble.Damage(damageAmout, _dir);
                    Destroy(gameObject, _particleSystem.main.duration);
                }

                break;
            }

            else if (collision.transform.CompareTag("Projectile")) return;
            else
                Destroy(gameObject);
        }

    }
}
