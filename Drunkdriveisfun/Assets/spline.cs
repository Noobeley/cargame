using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spline : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float maxPerpendicularMovement = 2f;
    public float boundsLimit = 10f;

    private GameObject[] pathObjects;
    private int currentPathIndex = 0;
    private bool isMovingPerpendicular = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with the "Path" tag
        pathObjects = GameObject.FindGameObjectsWithTag("Path");
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the closest path object
        MoveTowardsPathObject();

        // Check for input to move perpendicular
        CheckPerpendicularMovement();
    }

    void MoveTowardsPathObject()
    {
        if (pathObjects.Length == 0)
        {
            Debug.Log("No path objects found.");
            return;
        }

        // Get the closest path object
        GameObject closestPathObject = GetClosestPathObject();

        // Calculate the direction towards the closest path object
        Vector3 direction = closestPathObject.transform.position - transform.position;
        direction.Normalize();

        // Move towards the closest path object
        transform.position += direction * speed * Time.deltaTime;

        // Rotate towards the closest path object
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the capsule has reached the closest path object
        if (Vector3.Distance(transform.position, closestPathObject.transform.position) < 0.1f)
        {
            // Ignore the current path object and move towards the next one
            currentPathIndex++;
            if (currentPathIndex >= pathObjects.Length)
            {
                currentPathIndex = 0;
            }
        }
    }

    GameObject GetClosestPathObject()
    {
        GameObject closestPathObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject pathObject in pathObjects)
        {

            float distance = Vector3.Distance(transform.position, pathObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPathObject = pathObject;
            }
        }

        return closestPathObject;
    }

    void CheckPerpendicularMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0f)
        {
            isMovingPerpendicular = true;

            // Calculate the perpendicular direction based on the current forward direction
            Vector3 forwardDirection = transform.forward;
            Vector3 rightDirection = Vector3.Cross(forwardDirection, Vector3.up).normalized;

            // Calculate the movement amount based on the input and limits
            float movementAmount = horizontalInput * maxPerpendicularMovement;
            movementAmount = Mathf.Clamp(movementAmount, -maxPerpendicularMovement, maxPerpendicularMovement);

            // Move the capsule perpendicular to the current forward direction
            transform.position += rightDirection * movementAmount * Time.deltaTime;

            // Clamp the position within the bounds limit
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -boundsLimit, boundsLimit);
            transform.position = clampedPosition;
        }
        else
        {
            isMovingPerpendicular = false;
        }
    }
}
