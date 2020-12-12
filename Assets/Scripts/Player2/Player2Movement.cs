using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to deal with the second player's movement
/// </summary>

public class Player2Movement : MonoBehaviour
{
    [Tooltip("Speed of the second player")]
    public float speed = 10f;

    private float _maxHeight = 4f;
    private float _minHeight = -4f; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

            if (transform.position.y > _maxHeight)
                transform.position = new Vector3(transform.position.x, _maxHeight);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);

            if (transform.position.y < _minHeight)
                transform.position = new Vector3(transform.position.x, _minHeight);
        }

        else
        {
            return;
        }
    }
}
