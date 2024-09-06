using UnityEngine;
using UnityEngine.UIElements;

public class CarBehavior : MonoBehaviour
{
    public float maxMotorTorque = 1500f;  // Maximum torque applied to the driving wheels
    public float maxSteeringAngle = 45f;  // Maximum angle the wheels can steer
    public float maxReverseSteeringAngle = -45f;  // Maximum angle the wheels can steer in reverse
    public float handbrakeTorque = 2000f;  // Torque applied to the handbrake for drifting

    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    private float steeringAngle;
    private float motorTorque;
    private bool isHandbrakeApplied;
    private bool isReversing;
    private bool isFlipped;
    private float flipTimer; // Timer to track how long the car has been flipped

    private Vector3 lastCheckpoint;
    private Vector3[] pastCheckpoints;

    private void Start()
    {
        pastCheckpoints = new Vector3[0];
        flipTimer = 0f;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        CheckFlip();
    }

    private void GetInput()
    {
        steeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
        isHandbrakeApplied = Input.GetKey(KeyCode.Space); // Change to Spacebar
        isReversing = Input.GetAxis("Vertical") < 0;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    private void Steer()
    {
            frontLeftWheel.steerAngle = steeringAngle;
            frontRightWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        if (isHandbrakeApplied)
        {
            ApplyHandbrake();
        }
        else
        {
            frontLeftWheel.motorTorque = motorTorque;
            frontRightWheel.motorTorque = motorTorque;
            ReleaseHandbrake();
        }
    }

    private void ApplyHandbrake()
    {
        rearLeftWheel.brakeTorque = handbrakeTorque;
        rearRightWheel.brakeTorque = handbrakeTorque;
    }

    private void ReleaseHandbrake()
    {
        rearLeftWheel.brakeTorque = 0f;
        rearRightWheel.brakeTorque = 0f;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        transform.position = pos;
        transform.rotation = quat;
    }

    private void CheckFlip()
    {
        if (transform.up.y < 0f && !isFlipped)
        {
            flipTimer += Time.deltaTime; // Increment the flip timer

            if (flipTimer >= 3f) // If the car has been flipped for more than 3 seconds
            {
                isFlipped = true;
                StartCoroutine(FlipCar());
            }
        }
        else
        {
            flipTimer = 0f; // Reset the flip timer if the car is not flipped
        }
    }

    private System.Collections.IEnumerator FlipCar()
    {
        // Apply force to flip the car
        GetComponent<Rigidbody>().AddForce(Vector3.up * 500f, ForceMode.Impulse);

        // Wait for a short duration
        yield return new WaitForSeconds(1f);

        // Reset the car's rotation
        transform.rotation = Quaternion.identity;

        // Reset the flipped flag
        isFlipped = false;

        // Teleport to the nearest respawn point
        Respawn();
    }

    private void Respawn()
    {
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        if (respawnPoints.Length > 0)
        {
            Transform nearestRespawn = GetNearestRespawn(respawnPoints);
            if (nearestRespawn != null)
            {
                if (pastCheckpoints.Length > 0)
                {
                    pastCheckpoints = AddCheckpoint(pastCheckpoints, lastCheckpoint);
                    OutputCheckpoints(pastCheckpoints); // Output all coordinates stored in lastCheckpoints
                }
                lastCheckpoint = nearestRespawn.position;
                transform.position = lastCheckpoint;

                // Calculate the forward direction
                Vector3 forwardDirection = nearestRespawn.position - lastCheckpoint;
                forwardDirection.y = 0f; // Ignore the vertical component
                forwardDirection.Normalize();

                // Rotate the car to face forward
                transform.rotation = Quaternion.LookRotation(forwardDirection);

                // Reset the car's momentum
                Rigidbody carRigidbody = GetComponent<Rigidbody>();
                carRigidbody.velocity = Vector3.zero;
                carRigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    private Transform GetNearestRespawn(GameObject[] respawnPoints)
    {
        Transform nearestRespawn = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject respawnPoint in respawnPoints)
        {
            // Skip the current respawn point
            if (respawnPoint.transform.position == currentPosition)
            {
                continue;
            }

            float distance = Vector3.Distance(currentPosition, respawnPoint.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestRespawn = respawnPoint.transform;
            }
        }

        return nearestRespawn;
    }

    private Vector3[] AddCheckpoint(Vector3[] checkpoints, Vector3 checkpoint)
    {
        Vector3[] newCheckpoints = new Vector3[checkpoints.Length + 1];
        for (int i = 0; i < checkpoints.Length; i++)
        {
            newCheckpoints[i] = checkpoints[i];
        }
        newCheckpoints[checkpoints.Length] = checkpoint;
        return newCheckpoints;
    }

    private void OutputCheckpoints(Vector3[] checkpoints)
    {
        foreach (Vector3 checkpoint in checkpoints)
        {
            Debug.Log(checkpoint.ToString());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Vector3 checkpointPosition = other.transform.position;
            pastCheckpoints = AddCheckpoint(pastCheckpoints, checkpointPosition);
            OutputCheckpoints(pastCheckpoints);
        }
    }
}

