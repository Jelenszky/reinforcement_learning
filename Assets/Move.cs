using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 direction = Vector3.back; // Direction of movement
    public float speed = 5.0f; // Speed of movement

    void Update()
    {
        // Calculate the amount to move based on direction and speed
        float distanceToMove = speed * Time.deltaTime;
        Vector3 moveVector = direction.normalized * distanceToMove;

        // Move the object
        transform.Translate(moveVector);
    }
}
