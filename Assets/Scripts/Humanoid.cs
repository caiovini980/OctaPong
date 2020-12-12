using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to store the max health of the players 
/// </summary>

public class Humanoid : MonoBehaviour
{
    [Tooltip("Max amount of health a player can have")]
    public int maxHealth = 100;
}
