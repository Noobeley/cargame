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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision detected with " + other.gameObject.name);
        }
    }
}
