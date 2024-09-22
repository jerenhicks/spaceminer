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

    [SerializeField]
    ParticleSystem engineOne;
    [SerializeField]
    ParticleSystem engineTwo;
    [SerializeField]
    GameObject gamestateObj;

    private GameState gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = gamestateObj.GetComponent<GameState>();
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
