using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spline : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;
    public float boundsLimit = 10f;

    private List<GameObject> pathObjects;
    private int currentPathIndex = 0;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with the "Respawn" tag and add them to the pathObjects list
        pathObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Respawn"));
        if (pathObjects.Count > 0)
        {
            targetPosition = pathObjects[0].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target position
        MoveTowardsTargetPosition();

        // Check for new objects with the "Respawn" tag and add them to the pathObjects list
        GameObject[] newObjects = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject obj in newObjects)
        {
            if (!pathObjects.Contains(obj))
            {
                pathObjects.Add(obj);
            }
        }
    }

    void MoveTowardsTargetPosition()
    {
        if (pathObjects.Count == 0)
        {
            Debug.Log("No path objects found.");
            return;
        }

        // Calculate the direction towards the target position
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        // Move towards the target position
        transform.position += direction * speed * Time.deltaTime;

        // Rotate towards the target position
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the capsule has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Ignore the current target position and move towards the next one
            if (currentPathIndex >= pathObjects.Count)
            {
                currentPathIndex = 0;
            }
            pathObjects.RemoveAt(currentPathIndex);
            if (currentPathIndex >= pathObjects.Count)
            {
                currentPathIndex = 0;
            }
            if (pathObjects.Count > 0)
            {
                targetPosition = pathObjects[currentPathIndex].transform.position;
            }
        }
    }
}
