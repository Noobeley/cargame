using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Invoke the method to delete the object after 1 second
        Invoke("DeleteObject", 1f);
    }

    // Method to delete the object
    void DeleteObject()
    {
        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
