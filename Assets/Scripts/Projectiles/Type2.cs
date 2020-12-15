using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2 : MonoBehaviour
{
    public float type2Speed = 10f;
    public int type2Damage = 20;

    private Projectile _projectile;
    private PlayerHealth _p1Health, _p2Health;
    private string _PLAYER_1_NAME_TAG = "Player1";
    private string _PLAYER_2_NAME_TAG = "Player2";
    private float _scaleX = 1f;
    private float _scaleY = 1f;
    private float _scaleZ = 1f;
    private Vector3 _newScale;

    void Awake()
    {
        _projectile = gameObject.GetComponent<Projectile>();
        _p1Health = GameObject.Find(_PLAYER_1_NAME_TAG).GetComponent<PlayerHealth>();
        _p2Health = GameObject.Find(_PLAYER_2_NAME_TAG).GetComponent<PlayerHealth>();
    }

    void Start()
    {
        _projectile.SetProjectileSpeed(type2Speed);
        _newScale = new Vector3(1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.localScale += _newScale;

        if (collision.transform.CompareTag(_PLAYER_1_NAME_TAG))
        {
            _p1Health.DamagePlayer1(type2Damage);
        }

        if (collision.transform.CompareTag(_PLAYER_2_NAME_TAG))
        {
            _p2Health.DamagePlayer1(type2Damage);
        }
    }
}
