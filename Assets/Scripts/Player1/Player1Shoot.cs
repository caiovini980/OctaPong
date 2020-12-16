using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the shooting function of the first player
/// </summary>

public class Player1Shoot : MonoBehaviour
{
    [Tooltip("Prefab used for the bullet")]
    public GameObject projectilePrefab;

    [Tooltip("Position from where will shoot the bullet")]
    public Transform shotPosition;

    [Tooltip("Position of who is aiming")]
    public Transform aimPosition;

    private bool _canShoot = false;

    // Update is called once per frame
    void Update()
    {
        if (_canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                //instantiate particle effect
                //instantiate sound effect
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, shotPosition.position, Quaternion.identity);

        Vector3 shootDir = (shotPosition.position - aimPosition.position).normalized;
        bullet.GetComponent<Projectile>().Setup(shootDir);
        Debug.Log(shootDir);
    }

    public void CanShoot(bool status)
    {
        _canShoot = status;
    }
}
