using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carhitBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Beer")
        {
            Destroy(other.gameObject);
        }
    }
}
