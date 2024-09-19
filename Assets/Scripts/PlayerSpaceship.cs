using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpaceship : MonoBehaviour
{
    Rigidbody spaceshipRB;

    //Inputs
    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;
    float pitchUpDownInput;
    float pitchLeftRightInput;

    //Speed Multipliers
    [SerializeField]
    float speedMultiplier = 1.0f;
    [SerializeField]
    float speedMultiplierAngle = 0.5f;
    [SerializeField]
    float speedRollMultiplierAngle = 0.05f;
    [SerializeField]
    float speedPitchUpDownMultiplier = 0.5f;
    [SerializeField]
    float speedPitchLeftRightMultiplier = 0.5f;
    [SerializeField]
    ParticleSystem engineOne;
    [SerializeField]
    ParticleSystem engineTwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        spaceshipRB = GetComponent<Rigidbody>();
        engineOne.Stop();
        engineTwo.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
        pitchUpDownInput = Input.GetAxis("PitchUpDown");
        pitchLeftRightInput = Input.GetAxis("PitchLeftRight");
    }

    void FixedUpdate()
    {
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMultiplier, ForceMode.VelocityChange);
        spaceshipRB.AddForce(spaceshipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMultiplier, ForceMode.VelocityChange);

        spaceshipRB.AddTorque(spaceshipRB.transform.right * pitchUpDownInput * speedPitchUpDownMultiplier, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.up * pitchLeftRightInput * speedPitchLeftRightMultiplier, ForceMode.VelocityChange);
        spaceshipRB.AddTorque(spaceshipRB.transform.forward * speedRollMultiplierAngle * rollInput, ForceMode.VelocityChange);

        // Cap the speed
        float maxSpeed = 10.0f; // Set your desired max speed here
        if (spaceshipRB.linearVelocity.magnitude > maxSpeed)
        {
            spaceshipRB.linearVelocity = spaceshipRB.linearVelocity.normalized * maxSpeed;
        }

        // Play or stop particle systems based on forward motion input
        if (verticalMove > 0)
        {
            if (!engineOne.isPlaying)
            {
                engineOne.Play();
            }
            if (!engineTwo.isPlaying)
            {
                engineTwo.Play();
            }
        }
        else
        {
            if (engineOne.isPlaying)
            {
                engineOne.Stop();
            }
            if (engineTwo.isPlaying)
            {
                engineTwo.Stop();
            }
        }
    }
}
