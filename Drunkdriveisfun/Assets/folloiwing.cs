using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class folloiwing : MonoBehaviour
{
    public GameObject car; // Reference to the car object
    public float teleportOffset = 0.0f; // Offset for teleportation

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the teleport position based on the car's position and the teleport offset
        Vector3 teleportPosition = car.transform.position + teleportOffset * transform.right;

        // Teleport the object to the calculated position
        transform.position = teleportPosition;

        // Adjust the teleport offset based on input
        float input = Input.GetAxis("Horizontal");
        teleportOffset += input * 10 * Time.deltaTime;

        // Cap the teleport offset between 0 and 1
        //teleportOffset = Mathf.Clamp01(teleportOffset);
    }
}
