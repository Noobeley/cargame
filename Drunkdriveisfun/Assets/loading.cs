using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Call the method to delete the object
            DeleteObject();
        }
    }

    // Method to delete the object
    void DeleteObject()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}
