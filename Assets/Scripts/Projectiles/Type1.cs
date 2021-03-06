﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1 : MonoBehaviour
{
    [Tooltip("Speed of this type os projectile")]
    public float type1Speed = 20f;

    [Tooltip("Damage that this type of projectile causes")]
    public int type1Damage = 25;

    [SerializeField]
    private int bounces = 0;
    private Projectile _projectile;
    private PlayerHealth _p1Health, _p2Health;
    private string _PLAYER_1_NAME_TAG = "Player1";
    private string _PLAYER_2_NAME_TAG = "Player2";

    void Awake()
    {
        _projectile = gameObject.GetComponent<Projectile>();
        _p1Health = GameObject.Find(_PLAYER_1_NAME_TAG).GetComponent<PlayerHealth>();
        _p2Health = GameObject.Find(_PLAYER_2_NAME_TAG).GetComponent<PlayerHealth>();
    }

    void Start()
    {
        _projectile.SetProjectileSpeed(type1Speed);
    }

    private void Update()
    {
        if (bounces == 3)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(_PLAYER_2_NAME_TAG))
        {
            _p2Health.DamagePlayer2(type1Damage);
        }
    }

    public void SetBounces(int bounce)
    {
        bounces = bounce;
    }
}
