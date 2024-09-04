using UnityEngine;

public class CarBehavior : MonoBehaviour
{
    public float maxMotorTorque = 1500f;  // Maximum torque applied to the driving wheels
    public float maxSteeringAngle = 30f;  // Maximum angle the wheels can steer

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

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private void GetInput()
    {
        steeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        motorTorque = maxMotorTorque * Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontLeftWheel.motorTorque = motorTorque;
        frontRightWheel.motorTorque = motorTorque;
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
}

