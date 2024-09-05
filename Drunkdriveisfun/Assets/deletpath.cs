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
        if (pedest.CompareTag ("Path"))
        {
            Destroy(pedest.gameObject);
            Debug.Log("Path object destroyed.");
        }
    }


}
