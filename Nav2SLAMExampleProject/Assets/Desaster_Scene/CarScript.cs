using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public Rigidbody rigid;
    public WheelCollider wheel1, wheel2, wheel3, wheel4;
    public float drivespeed = 1500f;
    public float steerspeed = 30f;
    float horizontalInput, verticalInput;

    [Header("Car Body Rotation")]
    public Transform carBodyVisual;
    public float maxRollAngle = 5f;
    public float maxPitchAngle = 3f;
    public float tiltSmoothing = 8f;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        UpdateBodyRotation();
    }

    void FixedUpdate()
    {
        float motor = verticalInput * drivespeed;

        // Drive ALL 4 wheels (eliminates non-driven wheel lockup)
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        // Steer front wheels
        float steer = steerspeed * horizontalInput;
        wheel1.steerAngle = 0f;
        wheel2.steerAngle = steer;

        wheel3.steerAngle = 0f;
        wheel4.steerAngle = steer;
    }

    void UpdateBodyRotation()
    {
        if (carBodyVisual == null) return;

        float targetRoll = -horizontalInput * maxRollAngle;
        float targetPitch = verticalInput * maxPitchAngle;

        Quaternion targetRotation = Quaternion.Euler(targetPitch, 0f, targetRoll);

        carBodyVisual.localRotation = Quaternion.Slerp(
            carBodyVisual.localRotation,
            targetRotation,
            Time.deltaTime * tiltSmoothing
        );
    }
}