using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the projectile direction and speed
/// </summary>

public class Projectile : MonoBehaviour
{
    [Tooltip("Speed of the shot projectile")]
    public float shotSpeed = 20f;

    private float _timeToDestroyBullet = 2f;
    private Vector3 _shootDir;

    public void Setup(Vector3 shootDirection)
    {
        _shootDir = shootDirection;

        Destroy(gameObject, _timeToDestroyBullet);
        
    }

    private void Update()
    {
        transform.position += _shootDir * shotSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.contacts[0].normal);
        //instantiate particle effect
        //instantiate sound effect
    }

    void Bounce(Vector3 collisionNormal)
    {
        var bounceSpeed = _shootDir.magnitude;
        var direction = Vector3.Reflect(_shootDir.normalized, collisionNormal);

        _shootDir = direction * bounceSpeed;
    }
}
