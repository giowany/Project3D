using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int damageAmout = 1;
    public float speed = 50f;
    public List<string> tagsToHit;

    private Vector3 _dir = Vector3.zero;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
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
                var damageble = collision.transform.GetComponent<IDamageable>();
                if (damageble != null)
                {
                    if (collision.transform.CompareTag(tagsToHit[0]))
                    {
                        _dir = collision.transform.position - transform.position;
                        _dir = -_dir.normalized;
                        _dir.y = 0;

                    }

                    damageble.Damage(damageAmout, _dir);
                }

                break;
            }
        }

        if (!collision.transform.CompareTag("Projectile"))
            Destroy(gameObject);
    }
}
