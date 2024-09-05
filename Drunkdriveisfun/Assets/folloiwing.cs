using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class folloiwing : MonoBehaviour
{
    public GameObject car; // Reference to the car object
    public float teleportOffset = 0.0f; // Offset for teleportation
    private float lastMoveTime; // Time when the camera last moved
    private float levelStartTime; // Time when the level started

    // Start is called before the first frame update
    void Start()
    {
        lastMoveTime = Time.time; // Initialize the last move time
        levelStartTime = Time.time; // Initialize the level start time
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the teleport position based on the car's position and the teleport offset
        Vector3 teleportPosition = car.transform.position + teleportOffset * transform.right;

        // Teleport the object to the calculated position
        transform.position = teleportPosition;

        // Adjust the teleport offset based on mouse location
        float mousePositionX = Input.mousePosition.x;
        float screenWidth = Screen.width;
        float normalizedMousePositionX = (mousePositionX / screenWidth) * 20f - 10f;
        teleportOffset = normalizedMousePositionX;

        // Cap the teleport offset between -10 and 10
        teleportOffset = Mathf.Clamp(teleportOffset, -10f, 10f);

        // Check if 40 seconds have passed since the level started
        if (Time.time - levelStartTime > 40f)
        {
            SceneManager.LoadScene("MainMenu"); // Restart the scene called Main Menu
        }

        // Check if the camera has stopped moving for longer than 1 second
        if (Time.time - lastMoveTime > 1f)
        {
            lastMoveTime = Time.time; // Update the last move time
        }
    }
}
