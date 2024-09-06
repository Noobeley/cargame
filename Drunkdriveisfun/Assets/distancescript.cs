using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class distancescript : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 lastPosition;
    public TMPro.TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = Vector3.Distance(startPosition, currentPosition);
        distanceText.text = distanceMoved.ToString("F2");
        lastPosition = currentPosition;
    }
}
