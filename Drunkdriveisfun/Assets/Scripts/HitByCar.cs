using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByCar : MonoBehaviour
{
    [SerializeField] private GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        Debug.Log("XDDDD");
        if (player.CompareTag("Player"))
        {
            Debug.Log("KILL");
            if (!gameObject.CompareTag("Tree"))
            {
                Destroy(gameObject);
            }
            GameObject explosion = Instantiate(blood, transform.position, transform.rotation);
        }
    }
}
