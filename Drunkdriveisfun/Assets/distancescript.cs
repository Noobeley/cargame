using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class distancescript : MonoBehaviour
{
    private float totalDistance;
    private Vector3 lastPosition;
    public TMPro.TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        totalDistance = 0f;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
        totalDistance += distanceMoved;
        lastPosition = currentPosition;

        distanceText.text = totalDistance.ToString("F2");
    }
}
