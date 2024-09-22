using UnityEngine;
using System.Collections.Generic;

public class CharacterSwap : MonoBehaviour
{
    public Transform ship;
    public Transform player;
    public List<GameObject> hatches = new List<GameObject>();
    public float distanceToHatch = 4.0f;
    public GameState gameState;

    private PlayerMovement shipMovement;
    private PlayerMovement playerMovement;
    private CameraRotator cameraRotator;

    void Start()
    {
        shipMovement = ship.GetComponent<PlayerMovement>();
        playerMovement = player.GetComponent<PlayerMovement>();
        cameraRotator = gameState.GetComponent<CameraRotator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (shipMovement.enabled || IsPlayerNearHatch())
            {
                Swap();
            }
        }
    }

    public void Swap()
    {
        if (shipMovement != null)
        {
            //ship.gameObject.SetActive(!ship.gameObject.activeSelf);
            shipMovement.enabled = !shipMovement.enabled;
        }

        if (playerMovement != null)
        {
            // Move the player to the position of the first hatch if there are any hatches
            if (hatches.Count > 0)
            {
                player.position = hatches[0].transform.position;
            }

            player.gameObject.SetActive(!player.gameObject.activeSelf);
            playerMovement.enabled = !playerMovement.enabled;

            if (playerMovement.enabled)
            {
                cameraRotator.SetTarget(player);
            }
            else
            {
                cameraRotator.SetTarget(ship);
            }
        }
    }

    private bool IsPlayerNearHatch()
    {
        foreach (GameObject hatch in hatches)
        {
            if (Vector3.Distance(player.position, hatch.transform.position) <= distanceToHatch)
            {
                return true;
            }
        }
        return false;
    }

}
