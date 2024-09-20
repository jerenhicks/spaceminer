using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody player;

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
    GameObject gamestateObj;

    private GameState gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = gamestateObj.GetComponent<GameState>();
        player = GetComponent<Rigidbody>();

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
        player.AddForce(player.transform.TransformDirection(Vector3.forward) * verticalMove * speedMultiplier, ForceMode.VelocityChange);
        player.AddForce(player.transform.TransformDirection(Vector3.right) * horizontalMove * speedMultiplier, ForceMode.VelocityChange);

        player.AddTorque(player.transform.right * pitchUpDownInput * speedPitchUpDownMultiplier, ForceMode.VelocityChange);
        player.AddTorque(player.transform.up * pitchLeftRightInput * speedPitchLeftRightMultiplier, ForceMode.VelocityChange);
        player.AddTorque(player.transform.forward * speedRollMultiplierAngle * rollInput, ForceMode.VelocityChange);

        // Cap the speed
        float maxSpeed = 10.0f; // Set your desired max speed here
        if (player.linearVelocity.magnitude > maxSpeed)
        {
            player.linearVelocity = player.linearVelocity.normalized * maxSpeed;
        }
    }
}
