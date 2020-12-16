using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script created to deal with the damage taken by the players
/// </summary>

public class PlayerHealth : Humanoid
{
    [Tooltip("Object that has the HealthBar script")]
    public HealthBar _healthBar;

    [Tooltip("Panel responsible too show that Player 1 has won")]
    public GameObject player1WonPanel;

    [Tooltip("Panel responsible too show that Player 2 has won")]
    public GameObject player2WonPanel;

    [Tooltip("Amount of health that this player currently have")]
    public int currentHealth;
    
    private string _PLAYER_1_NAME = "Player1";
    private string _PLAYER_2_NAME = "Player2";
    private string _PROJECTILE_TAG1 = "Projectile1";
    private string _PROJECTILE_TAG2 = "Projectile2";
    

    private void Start()
    {
        Time.timeScale = 1;
        currentHealth = maxHealth;
        _healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Player 1 damage taken code
        if (gameObject.name == _PLAYER_1_NAME)
        {
            if (collision.transform.CompareTag(_PROJECTILE_TAG2))
            {
                _healthBar.SetHealth(currentHealth);
                Destroy(collision.gameObject);

                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                    player2WonPanel.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
        #endregion

        #region Player 2 damage taken code
        else if (gameObject.name == _PLAYER_2_NAME)
        {
            if (collision.transform.CompareTag(_PROJECTILE_TAG1))
            {
                _healthBar.SetHealth(currentHealth);
                Destroy(collision.gameObject);

                if (currentHealth <= 0)
                {
                    gameObject.SetActive(false);
                    player1WonPanel.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }
        #endregion
    }

    public void DamagePlayer1(int damage)
    {
        currentHealth = currentHealth - damage;
    }

    public void DamagePlayer2(int damage)
    {
        currentHealth = currentHealth - damage;
    }
}
