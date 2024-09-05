using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        // Invoke the method to delete the object after 1 second
        Invoke("DeleteObject", 1f);
=======

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
>>>>>>> new-alternate-sly-branch-2024
    }

    // Method to delete the object
    void DeleteObject()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
<<<<<<< HEAD

    // Update is called once per frame
    void Update()
    {

    }
=======
>>>>>>> new-alternate-sly-branch-2024
}
