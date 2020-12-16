using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to create functions for the health bar's slider, allowing to set it from another
/// script
/// </summary>

public class HealthBar : PlayerHealth
{
    [Tooltip("Place the slider that will represent the player's health bar")]
    public Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
