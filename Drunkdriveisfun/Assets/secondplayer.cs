using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondplayer : MonoBehaviour
{
    public LineRenderer beamRenderer; // Reference to the LineRenderer component
    public Transform beamOrigin; // The starting point of the beam

    void Start()
    {
        // Initialize the LineRenderer component
        beamRenderer = GetComponent<LineRenderer>();
        beamRenderer.startColor = Color.yellow;
        beamRenderer.endColor = Color.yellow;
        beamRenderer.startWidth = 0.1f;
        beamRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world coordinates
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        // Calculate the direction from the beam origin to the world mouse position
        Vector3 direction = (worldMousePosition - beamOrigin.position).normalized;

        // Calculate the curve of the beam
        Vector3[] curvePoints = CalculateStraightBeam(direction);

        // Update the LineRenderer with the curve points
        beamRenderer.positionCount = curvePoints.Length;
        beamRenderer.SetPositions(curvePoints);

        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the beam origin in the direction of the beam
            RaycastHit hit;
            if (Physics.Raycast(beamOrigin.position, direction, out hit, Mathf.Infinity)) // Increase the raycast distance to infinity
            {
                // Check if the ray hits an object
                if (hit.collider != null && hit.collider.CompareTag("Finish"))
                {
                    // Destroy the object
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    Vector3[] CalculateStraightBeam(Vector3 direction)
    {
        // Define the number of points in the beam
        int numPoints = 2;

        // Create an array to store the beam points
        Vector3[] beamPoints = new Vector3[numPoints];

        // Set the starting point of the beam
        beamPoints[0] = beamOrigin.position;

        // Set the end point of the beam
        beamPoints[1] = beamOrigin.position + direction * 10f;

        return beamPoints;
    }

    Vector3[] CalculateBeamCurve(Vector3 direction)
    {
        // Define the number of points in the curve
        int numPoints = 100;

        // Define the curve height
        float curveHeight = 2f;

        // Create an array to store the curve points
        Vector3[] curvePoints = new Vector3[numPoints];

        // Calculate the curve points
        for (int i = 0; i < numPoints; i++)
        {
            // Calculate the t value for the current point
            float t = (float)i / (numPoints - 1);

            // Calculate the x and y positions of the current point
            float x = t * direction.x * 10f;
            float y = curveHeight * Mathf.Sin(t * Mathf.PI);

            // Calculate the z position of the current point
            float z = t * direction.z * 10f;

            // Set the position of the current point in the curve
            curvePoints[i] = beamOrigin.position + new Vector3(x, y, z);
        }

        return curvePoints;
    }
}
