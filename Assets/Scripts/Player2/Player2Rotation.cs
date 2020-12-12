using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the second player's aim
/// </summary>

public class Player2Rotation : MonoBehaviour
{
    [Tooltip("Rotation of the second player")]
    public float rotationSpeed = 30f;

    [Tooltip("Maximum value a player can rotate to")]
    public Transform maxRotation;

    [Tooltip("Minimum value a player can rotate to")]
    public Transform minRotation;

    //private float _timeCount = 0f;

    // Update is called once per frame
    void Update()
    {
        #region Player rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, 1f) * rotationSpeed * Time.deltaTime);
            if (transform.rotation.z > maxRotation.rotation.z)
            {
                transform.rotation = maxRotation.rotation;
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0f, 0f, -1f) * rotationSpeed * Time.deltaTime);
            if (transform.rotation.z < minRotation.rotation.z)
            {
                transform.rotation = minRotation.rotation;
            }
        }
        #endregion

        else
        {
            return;
        }
    }
}
