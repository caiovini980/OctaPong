using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script created to deal with the damage taken by the players
/// </summary>

public class PlayerHealth : Humanoid
{
    [Tooltip("How much health the player currently have?")]
    [SerializeField] 
    private int _currentHealth;

    private string PLAYER_1_NAME = "Player1";
    private string PLAYER_2_NAME = "Player2";
    private string PROJECTILE_TAG = "Projectile";
    private HealthBar _healthBar;

    private void Awake()
    {
        if (gameObject.name == PLAYER_1_NAME)
        {
            _healthBar = GameObject.Find("HealthBarP1").GetComponent<HealthBar>();
        }

        else if (gameObject.name == PLAYER_2_NAME)
        {
            _healthBar = GameObject.Find("HealthBarP2").GetComponent<HealthBar>();
        }
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        _healthBar.SetMaxHealth(_currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Player 1 damage taken code
        if (gameObject.name == PLAYER_1_NAME)
        {
            if (collision.transform.CompareTag(PROJECTILE_TAG))
            {
                Debug.Log("collision on 1 detected");
                _currentHealth -= 20;
                _healthBar.SetHealth(_currentHealth);
                Destroy(collision.gameObject);

                if (_currentHealth == 0)
                {
                    Debug.Log("Player 2 wins");
                    gameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region Player 2 damage taken code
        else if (gameObject.name == PLAYER_2_NAME)
        {
            if (collision.transform.CompareTag(PROJECTILE_TAG))
            {
                Debug.Log("collision on 2 detected");
                _currentHealth -= 20;
                _healthBar.SetHealth(_currentHealth);
                Destroy(collision.gameObject);

                if (_currentHealth == 0)
                {
                    Debug.Log("Player 1 wins");
                    gameObject.SetActive(false);
                }
            }
        }
        #endregion
    }
}
