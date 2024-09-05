using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class deletpath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider pedest)
    {
        Debug.Log("Collision detected." + pedest.gameObject);
        if (pedest.CompareTag("Respawn"))
        {
            //pedest.gameObject.SetActive(false); // Disable the collided game object
           // Debug.Log("Path object disabled.");
            //enabled = false; // Disable the deletpath script
        }
    }
}
