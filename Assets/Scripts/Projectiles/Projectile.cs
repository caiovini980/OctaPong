using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the projectile direction and speed
/// </summary>

public class Projectile : MonoBehaviour
{
    [Tooltip("Speed of the shot projectile")]
    public float shotSpeed = 10f;
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;

    private float _timeToDestroyBullet = 2f;
    public int _bounce;

    private Vector3 _topBorder;
    private Vector3 _bottomBorder; 
    private Vector3 _rightBorder;
    private Vector3 _leftBorder;
    private Vector3 _normalizedTopVector;
    private Vector3 _normalizedBottomVector; 
    private Vector3 _normalizedRightVector;
    private Vector3 _normalizedLeftVector;
    private Vector3 _shootDir;

    private Type1 _type1;
    private Type2 _type2;

    private void Awake()
    {
        if (gameObject.CompareTag("Projectile1"))
        {
            _type1 = gameObject.GetComponent<Type1>();
        }

        if (gameObject.CompareTag("Projectile2"))
        {
            _type2 = gameObject.GetComponent<Type2>();
        }
    }

    private void Start()
    {
        _topBorder = B.position - A.position;
        _bottomBorder = D.position - C.position;
        _rightBorder = D.position - B.position; 
        _leftBorder = C.position - A.position;

        _normalizedTopVector = Vector3.Cross(_topBorder, Vector3.forward).normalized;
        _normalizedBottomVector = Vector3.Cross(_bottomBorder, Vector3.forward).normalized;
        _normalizedRightVector = Vector3.Cross(_rightBorder, Vector3.forward).normalized;
        _normalizedLeftVector = Vector3.Cross(_leftBorder, Vector3.forward).normalized;
    }

    public void Setup(Vector3 shootDirection)
    {
        _shootDir = shootDirection;

        Destroy(gameObject, _timeToDestroyBullet);
    }

    private void Update()
    {
        transform.position += _shootDir * shotSpeed * Time.deltaTime;
        StayInField();
    }

    void Bounce(Vector3 collisionNormal)
    {
        _bounce++;
        //setbounces

        if(gameObject.CompareTag("Projectile1"))
        {
            _type1.SetBounces(_bounce);
        }

        if (gameObject.CompareTag("Projectile2"))
        {
            _type2.SetBounces(_bounce);
        }

        var bounceSpeed = _shootDir.magnitude;
        var direction = Vector3.Reflect(_shootDir.normalized, collisionNormal);

        _shootDir = direction * bounceSpeed;
    }

    void StayInField()
    {
        if (transform.position.y >= 4.85f)
            Bounce(_normalizedTopVector);

        if (transform.position.y <= -4.85f)
            Bounce(_normalizedBottomVector);

        if (transform.position.x >= 8.74f)
            Bounce(_normalizedRightVector);

        if (transform.position.x <= -8.74f)
            Bounce(_normalizedLeftVector);
    }

    public void SetProjectileSpeed(float speed)
    {
        shotSpeed = speed;
    }

    public void SetTimeToDestroyTo10()
    {
        _timeToDestroyBullet = 10f;
    }
}
