using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftright : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the camera moves
    public float distanceLimit = 10f; // Distance limit on each side

    // Update is called once per frame
    void Update()
    {
        // Move the camera left or right based on input or another mechanism
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Clamp the camera's position within the distance limit on either side
        float clampedX = Mathf.Clamp(transform.position.x, -distanceLimit, distanceLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}