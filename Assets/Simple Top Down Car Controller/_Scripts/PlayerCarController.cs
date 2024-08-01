using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Tooltip("Force applied to drive the car forward.")]
    [SerializeField]
    float driveForce = 10f;

    [Tooltip("Maximum speed of the car.")]
    [SerializeField]
    float maxSpeed = 10f;

    [Tooltip("Force applied to drive the car in reverse.")]
    [SerializeField]
    float reverseForce = 10f;

    [Tooltip("Maximum reverse speed of the car.")]
    [SerializeField]
    float reverseMaxSpeed = 5f;

    [Tooltip("Force applied to stop the car.")]
    [SerializeField]
    float brakeForce = 10f;

    [Tooltip("steering value of the card.")]
    [SerializeField]
    float steer = 2;

    // This is the current actual force being applied to the car
    [SerializeField] // for testing in editor. [SerializeField] is not needed here in normal circumstances.
    float currentDriveForce = 0;

    // is the car reversing?
    bool reversing = false;

    // is the car accelerating?
    bool accelerating = false;

    // Note that I have divided the forces by 100f. This was the optimal value I found to drive the car smoothly. You can change this value to your liking.
    float forcesDivider = 100f;

    private void Update()
    {
        // Apply drive force
        if (Input.GetKey(KeyCode.W))
        {
            accelerating = true;

            if(currentDriveForce < 0)
                currentDriveForce += (brakeForce / forcesDivider);
            else
                currentDriveForce += (driveForce / forcesDivider);

            if (currentDriveForce >= maxSpeed)
            {
                currentDriveForce = maxSpeed;
            }

        }
        // Stop applying drive force
        else if (Input.GetKeyUp(KeyCode.W))
        {
            accelerating = false;
        }

        // Apply reverse force
        if(Input.GetKey(KeyCode.S))
        {
            reversing = true;

            if(currentDriveForce > 0)
            {
                currentDriveForce -= brakeForce / forcesDivider;
            }
            else
                currentDriveForce -= reverseForce / forcesDivider;

            if (currentDriveForce <= -reverseMaxSpeed)
                currentDriveForce = -reverseMaxSpeed;
        }
        // Stop applying reverse force
        else if (Input.GetKeyUp(KeyCode.S))
        {
            reversing = false;
        }

        // Engine braking
        if(!accelerating && !reversing)
        {
            if (currentDriveForce > 0)
                currentDriveForce -= (driveForce / forcesDivider);
            else if(currentDriveForce < 0)
                currentDriveForce += (reverseForce / forcesDivider);

            // This threshold (-0.05 - 0.05) is to ensure the value does not go crazy when reaching 0, and remains exactly 0
            // as float values usually have a lot of floating point numbers after the decimal place.
            if (currentDriveForce <= 0.05 && currentDriveForce >= -0.05f)
                currentDriveForce = 0;
        }

        // Apply current drive force
        transform.Translate(currentDriveForce * Time.deltaTime * Vector2.up);

        // Steering
        if (currentDriveForce >= 0)
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.forward * steer);
            else if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.back * steer);
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.forward * -steer);
            else if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.back * -steer);
        }
    }
}
