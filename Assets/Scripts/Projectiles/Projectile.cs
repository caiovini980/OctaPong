using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the projectile direction, speed and collisions with obstacles
/// </summary>

public class Projectile : MonoBehaviour
{
    
    [Tooltip("Speed of the shot projectile")]
    public float shotSpeed = 10f;

    [Header("Field corners: ")]
    public Transform A;
    public Transform B;
    public Transform C;
    public Transform D;

    [Header("Obstacle corners: ")]
    public Transform H;
    public Transform I;
    public Transform J;
    public Transform K;

    [Tooltip("Particle effect for the bounce")]
    public GameObject explosion;

    [Tooltip("How many times this projectile has bounced?")]
    public int bounce;

    private float _timeToDestroyBullet = 2f;
    private float _offset = 0.5f;
    private float _fieldOffsetX = 8.74f;
    private float _fieldOffsetY = 4.85f;

    private Type1 _type1;
    private Type2 _type2;

    private string _PROJECTILE1_TAG = "Projectile1";
    private string _PROJECTILE2_TAG = "Projectile2";

    private Vector3 _topBorder;
    private Vector3 _bottomBorder;
    private Vector3 _rightBorder;
    private Vector3 _leftBorder;
    private Vector3 _normalizedTopVector;
    private Vector3 _normalizedBottomVector;
    private Vector3 _normalizedRightVector;
    private Vector3 _normalizedLeftVector;

    private Vector3 _ObsBorderLeft;
    private Vector3 _ObsBorderRight;
    private Vector3 _ObsBorderTop;
    private Vector3 _ObsBorderBottom;
    private Vector3 _ObsNormalizedRightVector;
    private Vector3 _ObsNormalizedLeftVector;
    private Vector3 _ObsNormalizedTopVector;
    private Vector3 _ObsNormalizedBottomVector;

    private Vector3 _shootDir;

    private void Awake()
    {
        if (gameObject.CompareTag(_PROJECTILE1_TAG))
        {
            _type1 = gameObject.GetComponent<Type1>();
        }

        if (gameObject.CompareTag(_PROJECTILE2_TAG))
        {
            _type2 = gameObject.GetComponent<Type2>();
        }

    }

    private void Start()
    {
        #region Map field
        _topBorder = B.position - A.position;
        _bottomBorder = D.position - C.position;
        _rightBorder = D.position - B.position;
        _leftBorder = C.position - A.position;

        _normalizedTopVector = Vector3.Cross(_topBorder, Vector3.forward).normalized;
        _normalizedBottomVector = Vector3.Cross(_bottomBorder, Vector3.forward).normalized;
        _normalizedRightVector = Vector3.Cross(_rightBorder, Vector3.forward).normalized;
        _normalizedLeftVector = Vector3.Cross(_leftBorder, Vector3.forward).normalized;
        #endregion

        #region Obstacle
        _ObsBorderTop = I.position - H.position;
        _ObsBorderBottom = J.position - K.position; 
        _ObsBorderLeft = H.position - J.position; 
        _ObsBorderRight = K.position - I.position;

        Debug.Log(_ObsBorderTop);
        Debug.Log(_ObsBorderBottom);
        Debug.Log(_ObsBorderLeft);
        Debug.Log(_ObsBorderRight);

        _ObsNormalizedRightVector = Vector3.Cross(_ObsBorderRight, Vector3.forward).normalized;
        _ObsNormalizedLeftVector = Vector3.Cross(_ObsBorderLeft, Vector3.forward).normalized;
        _ObsNormalizedBottomVector = Vector3.Cross(_ObsBorderTop, Vector3.forward).normalized;
        _ObsNormalizedTopVector = Vector3.Cross(_ObsBorderBottom, Vector3.forward).normalized;
        #endregion
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
        StayOutOfObstacle();
    }

    void Bounce(Vector3 collisionNormal)
    {
        bounce++;
        //play particle system
        GameObject shotParticle = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(shotParticle, 3f);

        if (gameObject.CompareTag(_PROJECTILE1_TAG))
        {
            _type1.SetBounces(bounce);
        }

        if (gameObject.CompareTag(_PROJECTILE2_TAG))
        {
            _type2.SetBounces(bounce);
        }

        var bounceSpeed = _shootDir.magnitude;
        var direction = Vector3.Reflect(_shootDir.normalized, collisionNormal);

        _shootDir = direction * bounceSpeed;
    }

    void StayOutOfObstacle()
    {
        if (_shootDir.x > 0 &&
        transform.position.y <= H.position.y &&
        transform.position.y >= J.position.y && 
        transform.position.x >= H.position.x &&
        transform.position.x < H.position.x + _offset)
        {
            Bounce(_ObsNormalizedLeftVector);
        }

        else if (_shootDir.x < 0 &&
        transform.position.y <= H.position.y &&
        transform.position.y >= J.position.y && 
        transform.position.x <= K.position.x &&
        transform.position.x > K.position.x - _offset)
        {
            Bounce(_ObsNormalizedRightVector);
        }

        else if (_shootDir.y < 0 &&
        transform.position.x >= H.position.x &&
        transform.position.x <= I.position.x &&
        transform.position.y <= H.position.y &&
        transform.position.y > H.position.y - _offset)
        {
            Bounce(_ObsNormalizedTopVector);
        }

        else if (_shootDir.y > 0 &&
        transform.position.x >= H.position.x &&
        transform.position.x <= I.position.x &&
        transform.position.y >= J.position.y &&
        transform.position.y < J.position.y + _offset)
        {
            Bounce(_ObsNormalizedBottomVector);
        }
    }

    void StayInField()
    {
        if (transform.position.y >= _fieldOffsetY)
            Bounce(_normalizedTopVector);

        if (transform.position.y <= -_fieldOffsetY)
            Bounce(_normalizedBottomVector);

        if (transform.position.x >= _fieldOffsetX)
            Bounce(_normalizedRightVector);

        if (transform.position.x <= -_fieldOffsetX)
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
