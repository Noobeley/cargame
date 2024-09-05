using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedes : MonoBehaviour
{
    public Transform target; // The target position to move towards
    public float speed = 5f; // The speed of movement

    private Vector3 startPosition; // The starting position
    private bool movingForward = true; // Flag to indicate if the object is moving forward or backward

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Store the starting position
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction to move based on the current position and the target position
        Vector3 direction = movingForward ? (target.position - transform.position) : (startPosition - transform.position);
        direction.Normalize(); // Normalize the direction vector

        // Move the object towards the target or the starting position based on the direction and speed
        transform.position += direction * speed * Time.deltaTime;

        // Check if the object has reached the target or the starting position
        if (movingForward && Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            movingForward = false; // Change the direction to move backward
        }
        else if (!movingForward && Vector3.Distance(transform.position, startPosition) < 0.1f)
        {
            movingForward = true; // Change the direction to move forward
        }
    }
}
